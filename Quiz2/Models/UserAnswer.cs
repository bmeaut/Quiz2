using System.ComponentModel.DataAnnotations;

namespace Quiz2.Models
{
    public class UserAnswer {
        [Key]
        public int Id { get; set; }
        [Required]
        public Game Game { get; set; }
        [Required]
        public ApplicationUser ApplicationUser { get; set; }
       // [Required]
        public Answer Answer { get; set; }
        public int TimeOfSubmit { get; set; }
    }
}