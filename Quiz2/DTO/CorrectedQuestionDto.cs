using System.Collections.Generic;

namespace Quiz2.DTO
{
    public class CorrectedQuestionDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<CorrectedAnswerDto> Answers{ get; set; }
        public int Points { get; set; }
    }
}