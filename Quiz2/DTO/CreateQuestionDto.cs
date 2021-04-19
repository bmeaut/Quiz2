using System.Collections.Generic;
using Quiz2.Models;

namespace Quiz2.DTO
{
    public class CreateQuestionDto
    {
        public string Text { get; set; }
        public int QuizId { get; set; }
        public ICollection<CreateAnswerDto> CreateAnswerDtoList { get; set; }
        public int SecondsToAnswer { get; set; }
        public int Position { get; set; }
        public int Points { get; set; }
    }
}