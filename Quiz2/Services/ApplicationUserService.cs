using System.Collections.Generic;
using System.Linq;
using Quiz2.Data;
using Quiz2.DTO;
using Quiz2.Models;

namespace Quiz2.Services
{
    public class ApplicationUserService: IApplicationUserService
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public ApplicationUser GetUser(string userId)
        {
            return _context.ApplicationUsers.Find(userId);
        }

        public List<ApplicationUser> GetUsers()
        {
            return _context.ApplicationUsers.ToList();
        }

        public List<Game> GetGames(string userId)
        {
            var user = _context.ApplicationUsers.Find(userId);
            return _context.Games.Where(e => e.JoinedUsers.Contains(user)).ToList();
        }

        public Game JoinGame(string joinId)
        {
            return _context.Games.First(q => q.JoinId.Equals(joinId));
        }

        public ApplicationUser UpdateApplicationUser(string userId, UpdateApplicationUserDto updateApplicationUserDto)
        {
            var user = _context.ApplicationUsers.Find(userId);
            if (user == null)
            {
                return null;
            }
            user.UserName = updateApplicationUserDto.Name;
            _context.SaveChanges();
            return user;
        }
    }
}