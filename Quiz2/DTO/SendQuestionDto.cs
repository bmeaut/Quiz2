using System.Collections.Generic;

namespace Quiz2.DTO
{
    public class SendQuestionDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<SendAnswerDto> Answers{ get; set; }
        public int SecondsToAnswer { get; set; }
        public int Points { get; set; }
    }
}