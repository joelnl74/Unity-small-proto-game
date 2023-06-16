namespace Game
{
    public interface IHighScorePresenter
    {
        public void LoadData();
        public void SaveScore(int score);
    }
}
