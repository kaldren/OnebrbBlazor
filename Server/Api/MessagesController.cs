using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Onebrb.Server.Data;
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
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public MessagesController(
            ApplicationDbContext dbContext,
            IMessageRepository messageRepository,
            IUserRepository userRepository,
            IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _messageRepository = messageRepository;
            _userRepository = userRepository;
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
                        var result = await _messageRepository.GetAllSentMessages(currentUser.Id);
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
                var result = await _messageRepository.GetAll(currentUser.Id);
                messages = result.ToList();
            }

            var viewModel = _mapper.Map<List<MessageDto>>(messages);

            return Ok(viewModel);
        }

        /// <summary>
        /// Creates a new message
        /// </summary>
        /// <param name="model">CreateMessageViewModel</param>
        /// <returns>Message</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] MessageDto model)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var recipient = await _userRepository.GetUserByUserNameAsync(model.RecipientUserName);

            if (recipient == null)
            {
                return BadRequest(new { Message = $"Recipient {model.RecipientUserName} not found." });
            }

            model.RecipientId = recipient.Id;
            model.AuthorId = currentUser.Id;

            var message = _mapper.Map<Message>(model);

            await _dbContext.Messages.AddAsync(message);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = message.Id }, model);
        }
    }
}
