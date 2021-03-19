using Quiz2.Data;
using Quiz2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz2.Services
{
    public class ApplicationUserService
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public ApplicationUser GetUser(int userId)
        {
            return _context.ApplicationUsers.Find(userId);
        }
    }
}
