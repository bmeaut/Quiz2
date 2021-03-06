using System.Collections.Generic;
using Quiz2.Models;

namespace Quiz2.DTO
{
    public class UpdateQuestionDto
    {
        public string Text { get; set; }
        public int SecondsToAnswer { get; set; }
        public int Position { get; set; }
        public int Points { get; set; }
        public List<UpdateAnswerDto> Answers { get; set; }
    }
}