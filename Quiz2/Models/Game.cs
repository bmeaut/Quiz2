using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Quiz2.Models
{
    public class Game
    {
        [Key]
        public int GameId { get; set; }
        [Required]
        public Quiz Quiz { get; set; }
        public Question CurrentQuestion { get; set; }
        public ApplicationUser Owner { get; set; }
        public ICollection<GameUsers> JoinedUsers { get; set; }
        public string JoinId { get; set; }
    }
}