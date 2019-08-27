namespace Assets.Scripts.Workers.IO
{
    interface IScoreLoader
    {
        void SaveScore(string chapter, int level, int star, int score, GameResult result);
    }
}
