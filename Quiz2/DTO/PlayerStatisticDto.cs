using System.Collections.Generic;

namespace Quiz2.DTO
{
    public class PlayerStatisticDto
    {
        public int Id { get; set; }

        public string Text { get; set; }
    
        public List<AnswerWithMark> Answers { get; set; }
        
        public int Points { get; set; }
        
        public List<int> Stats  { get; set; }
    }
    public class AnswerWithMark
    {
        public int Id { get; set; }
        public bool Correct { get; set; }
        public bool Marked { get; set; }
        public string Text { get; set; }
    }
    
}