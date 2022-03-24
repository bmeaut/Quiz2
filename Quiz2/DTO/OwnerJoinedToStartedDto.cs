namespace Quiz2.DTO
{
    public class OwnerJoinedToStartedDto
    {
        public SendQuestionToOwnerDto Question { get; set; }
        public CurrentQuestionStatDto CurrentQuestionStat { get; set; }
        public int RemainingTime { get; set; }
        public string JoinId { get; set; }

    }
}