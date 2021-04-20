using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Quiz2.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        
        public int QuizId { get; set; }
        [Required] 
        public Quiz Quiz { get; set; }
        
        [Required]
        public string Text { get; set; }
       // [Required]
        public List<Answer> Answers { get; set; }
        public int SecondsToAnswer { get; set; }
        [Required]
        public int Position { get; set; }
        public int Points { get; set; }
    }
}