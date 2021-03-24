using Quiz2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quiz2.DTO;

namespace Quiz2.Services
{
    public interface IApplicationUserService
    {
        public ApplicationUser GetUser(string userId);
        public List<ApplicationUser> GetUsers();
        public List<Game> GetGames(string userId);
        public Game JoinGame(string joinId);
        public ApplicationUser UpdateApplicationUser(string userId, UpdateApplicationUserDto updateApplicationUserDto);
    }
}