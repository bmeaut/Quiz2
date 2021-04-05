namespace Quiz2.DTO
{
    public class CreateAnswerDto
    {
        public string Text { get; set; }
        public bool Correct { get; set; }
        public int QuestionId { get; set; }
    }
}