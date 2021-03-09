using System.ComponentModel.DataAnnotations;

namespace Quiz2.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Quiz Quiz { get; set; }
        public Question CurrentQuestion { get; set; }
        public ApplicationUser Owner { get; set; }
        public string JoinId { get; set; }
    }
}