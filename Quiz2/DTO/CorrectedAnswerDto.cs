namespace Quiz2.DTO
{
    public class CorrectedAnswerDto
    {
        public int Id{ get; set; }
        public bool Correct{ get; set; }
        public bool Marked{ get; set; }
        public string Text { get; set; }
    }
}