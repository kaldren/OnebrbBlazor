using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Onebrb.Server.Interfaces;
using Onebrb.Server.Models;
using Onebrb.Shared.Dtos.Messages;

namespace Onebrb.Server.Api
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageRepository _repo;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public MessagesController(IMessageRepository repo, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _repo = repo;
            _mapper = mapper;
            _userManager = userManager;
        }

        /// <summary>
        /// Get all user messages 
        /// </summary>
        /// <returns>All of the user's messages</returns>
        [HttpGet("{show?}")]
        public async Task<IActionResult> Get([FromQuery] string show)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            List<Message> messages = new List<Message>();

            // Optional parameter
            if (!string.IsNullOrWhiteSpace(show))
            {
                switch (show)
                {
                    case "sent":
                        var result = await _repo.GetAllSentMessages(currentUser.Id);
                        messages = result.ToList();
                        break;
                    //case "received":
                    //    messages = await _db.Messages
                    //            .Where(x => x.ApplicationUserMessages
                    //                .Any(x => x.ApplicationUser.Id == currentUser.Id)
                    //                && x.RecipientId == currentUser.Id
                    //                && !x.IsDeletedForRecipient
                    //                && !x.IsArchivedForRecipient)
                    //            .ToListAsync();
                    //    break;
                    //case "archived":
                    //    messages = await _db.Messages
                    //            .Where(x => x.ApplicationUserMessages
                    //                .Any(x => x.ApplicationUser.Id == currentUser.Id)
                    //                && x.RecipientId == currentUser.Id
                    //                && x.IsArchivedForRecipient
                    //                && !x.IsDeletedForRecipient)
                    //            .ToListAsync();
                    //    break;
                    //default:
                    //    messages = await _db.Messages
                    //            .Where(x => x.ApplicationUserMessages
                    //                .Any(x => x.ApplicationUser.Id == currentUser.Id)
                    //                && x.AuthorId == currentUser.Id
                    //                && !x.IsDeletedForAuthor
                    //                && !x.IsArchivedForAuthor)
                    //            .ToListAsync();
                    //    break;
                }
            }
            else 
            {
                var result = await _repo.GetAll(currentUser.Id);
                messages = result.ToList();
            }

            var viewModel = _mapper.Map<List<MessageDto>>(messages);

            return Ok(viewModel);
        }
    }
}
