namespace Assets.Scripts.Workers.IO.Score
{
    interface IScoreWriter
    {
        void SaveScore(string chapter, int level, int star, int score, GameResult result);
    }
}