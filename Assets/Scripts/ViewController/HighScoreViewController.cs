namespace Game
{
    /// <summary>
    /// Controller for the high score view.
    /// </summary>
    public class HighScoreViewController : PlayerScoreViewController
    {
        private HighScorePresenter m_presenter = new HighScorePresenter();

        protected override void Start()
        {
            base.Start();

            // Bind view to presenter.
            m_presenter.Bind(m_view);
            m_presenter.LoadData();
        }

        protected override void OnGameStateChanged(GameState gameState)
        {
            if (gameState != GameState.Ending)
            {
                return;
            }

            m_presenter.SaveScore(m_score);
            m_presenter.LoadData();
        }
    }
}
