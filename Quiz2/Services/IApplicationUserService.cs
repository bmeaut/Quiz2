using Quiz2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz2.Services
{
    public interface IApplicationUserService
    {
        public ApplicationUser GetUser(string userId);
    }
}
