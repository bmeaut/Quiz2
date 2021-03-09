using System.ComponentModel.DataAnnotations;

namespace Quiz2.Models
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public bool Correct { get; set; }
        [Required]
        public Question Question { get; set; }
        [Required]
        public string Text { get; set; }
    }
}