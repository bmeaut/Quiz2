using System.ComponentModel.DataAnnotations;

namespace Quiz2.Models
{
    public class UserAnswer {
        [Key]
        public int Id { get; set; }
        
        public int GameId { get; set; }
        [Required]
        public Game Game { get; set; }
        
        public string ApplicationUserId { get; set; }
        [Required]
        public ApplicationUser ApplicationUser { get; set; }
        
        //public int AnswerId { get; set; }
        public Answer Answer { get; set; }
        
        public int TimeOfSubmit { get; set; }
    }
}