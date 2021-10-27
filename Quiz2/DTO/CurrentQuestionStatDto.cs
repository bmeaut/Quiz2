using System.Collections.Generic;
using Quiz2.Models;

namespace Quiz2.DTO
{
    public class CurrentQuestionStatDto
    {
        public List<CorrectedAnswerDto> CorrectedAnswers{ get; set; }
        public List<int> Stats{ get; set; }
        public int Point{ get; set; }
    }
}