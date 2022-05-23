using System.Collections.Generic;

namespace Quiz2.DTO
{
    public class CorrectedQuestionAndStatsDto
    {
        public List<CorrectedAnswerDto> CorrectedAnswers{ get; set; }
        public List<int> Stats{ get; set; }
        public int Point{ get; set; }
    }
}