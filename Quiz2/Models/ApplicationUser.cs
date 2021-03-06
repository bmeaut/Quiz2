using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz2.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Game> OwnGames { get; set; }
        public ICollection<Game> Games { get; set; }
    }
}
