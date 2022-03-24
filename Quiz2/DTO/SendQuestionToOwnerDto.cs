using System.Collections.Generic;

namespace Quiz2.DTO
{
    public class SendQuestionToOwnerDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<SendAnswerToOwnerDto> Answers{ get; set; }
        public int SecondsToAnswer { get; set; }
        public int Points { get; set; }
    }
}