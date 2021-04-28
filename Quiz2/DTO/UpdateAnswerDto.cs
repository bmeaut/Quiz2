using Quiz2.Models;

namespace Quiz2.DTO
{
    public class UpdateAnswerDto
    {
        public readonly int Id;
        public readonly int QuestionId;
        public bool Correct { get; set; }
        public string Text { get; set; }
    }
}