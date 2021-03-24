using System.Collections.Generic;
using Quiz2.Models;

namespace Quiz2.DTO
{
    public class CreateQuestionDto
    {
        public string Text { get; set; }
        public int QuizId { get; set; }
        //public ICollection<Answer> Answers { get; set; }
    }
}