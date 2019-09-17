using System;

namespace Assets.Scripts
{
    public enum ScoreThreshold { None, Low, Medium, High, VeryHigh }

    [Serializable]
    public class ScoreWord
    {
        public ScoreThreshold ScoreThreshold;
        public string[] Words; 
    

        public static ScoreThreshold GetScoreThreshold(int score)
        {
            if (score > 100)
            {
                return ScoreThreshold.VeryHigh;
            }
            else if (score > 65)
            {
                return ScoreThreshold.High;
            }
            else if(score > 35)
            {
                return ScoreThreshold.Medium;
            }
            else if (score > 15)
            {
                return ScoreThreshold.Low;
            }
            else
            {
                return ScoreThreshold.None;
            }
         }
    }
}
