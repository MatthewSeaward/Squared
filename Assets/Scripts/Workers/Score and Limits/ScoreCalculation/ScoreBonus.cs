namespace Assets.Scripts.Workers.Score_and_Limits.ScoreCalculation
{
    public class ScoreBonus
    {
        public float Multiplier { get; private set; }
        public int TurnsLeft { get; private set; }
        public bool Active => TurnsLeft > 0;
        public ScoreBonus(float muliplier, int turnsLeft)
        {
            this.Multiplier = muliplier;
            this.TurnsLeft = turnsLeft;                
        }

        public void TurnPast()
        {
            TurnsLeft--;
            if (TurnsLeft <= 0)
            {
                Multiplier = 1;
                TurnsLeft = 0;
            }
        }
    }
}
