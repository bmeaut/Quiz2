using System.ComponentModel.DataAnnotations;

namespace Quiz2.Models
{
    public class GameUsers
    {
        [Key]
        public int GameId { get; set; }
        public Game Game { get; set; }
        [Key]
        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}