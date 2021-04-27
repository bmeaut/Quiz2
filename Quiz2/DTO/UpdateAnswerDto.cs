using Quiz2.Models;

namespace Quiz2.DTO
{
    public class UpdateAnswerDto
    {
        public readonly int Id;
        public string Text { get; set; }
        public bool Correct { get; set; }
    }
}