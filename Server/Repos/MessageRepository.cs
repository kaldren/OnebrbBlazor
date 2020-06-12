using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Onebrb.Server.Abstractions;
using Onebrb.Server.Data;
using Onebrb.Server.Interfaces;
using Onebrb.Server.Models;
using Onebrb.Shared.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onebrb.Server.Repos
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public MessageRepository(ApplicationDbContext db, IMapper mapper, UserManager<ApplicationUser> userManager) : base(db)
        {
            _db = db; 
            _mapper = mapper;
            _userManager = userManager;
        }

        /// <summary>
        /// Gets all sent messages by user
        /// </summary>
        /// <param name="userId">The user id</param>
        /// <returns>List of messages</returns>
        public async Task<IEnumerable<Message>> GetAllSentMessages(int userId)
        {
            var currentUser = await _db.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (currentUser == null)
            {
                return Enumerable.Empty<Message>();
            }

            return await _db.Messages
                                .Where(x => x.ApplicationUserMessage
                                    .Any(x => x.ApplicationUser.Id == currentUser.Id)
                                    && x.AuthorId == currentUser.Id
                                    && !x.IsDeletedForAuthor
                                    && !x.IsArchivedForAuthor)
                                .ToListAsync();
        }
    }
}
