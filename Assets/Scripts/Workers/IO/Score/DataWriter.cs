namespace Assets.Scripts.Workers.IO.Data_Writer
{
    public class ScoreManager
    {
        private IScoreLoader _dataWriter = new FireBaseScoreLoader();

        private static ScoreManager _instance;
        
        public static ScoreManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ScoreManager();
                }

                return _instance;
            }
        }

        private ScoreManager() {    }

        ~ScoreManager()
        {
            ScoreKeeper.GameCompleted -= ScoreKeeper_GameCompleted;
        }

        public void Initialise()
        {
            ScoreKeeper.GameCompleted += ScoreKeeper_GameCompleted;

        }

        private void ScoreKeeper_GameCompleted(string chapter, int level, int star, int score, GameResult result)
        {
            _dataWriter.SaveScore(chapter, level, star, score, result);
        }
    }
}
