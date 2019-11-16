using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class ScoreDisplay : MonoBehaviour
    {
        [SerializeField]
        private Text Score;

        [SerializeField]
        private Text Bonus;

        [SerializeField]
        private ProgressBar ScoreProgress;

        private void Start()
        {
            ScoreKeeper.CurrentPointsChanged += UpdateScore;
            ScoreKeeper.BonusChanged += UpdateBonus;
        }

        private void OnDestroy()
        {
            ScoreKeeper.CurrentPointsChanged -= UpdateScore;
            ScoreKeeper.BonusChanged -= UpdateBonus;
        }

        private void UpdateScore(int newScore, int target)
        {
            Score.text = $"Score: {newScore}/{target}";
            Score.color = Color.white;

            ScoreProgress.UpdateProgressBar(newScore, target);
        }

        private void UpdateBonus(string bonusMultiplier)
        {
            if (string.IsNullOrEmpty(bonusMultiplier))
            {
                Bonus.gameObject.SetActive(false);
            }
            else
            {
                Bonus.gameObject.SetActive(true);
                Bonus.text = "Multiplier: " + bonusMultiplier + "x";
                Bonus.GetComponent<Animator>().SetTrigger("Activate");
            }
        }
    }
}
