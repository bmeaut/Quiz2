namespace Quiz2.Services
{
    public interface IStatService
    {
        public int GetNumberOfUserAnswers(int gameId, int answerId);
    }
}