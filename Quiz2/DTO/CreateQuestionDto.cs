using System.Collections.Generic;
using Quiz2.Models;

namespace Quiz2.DTO
{
    public class CreateQuestionDto
    {
        public CreateQuestionDto()
        {
            CreateAnswerDtoList = new List<CreateAnswerDto>();
        }
        public string Text { get; set; }
        public int QuizId { get; set; }
        public IList<CreateAnswerDto> CreateAnswerDtoList { get; set; }
        public int SecondsToAnswer { get; set; }
        public int Position { get; set; }
        public int Points { get; set; }
    }
}