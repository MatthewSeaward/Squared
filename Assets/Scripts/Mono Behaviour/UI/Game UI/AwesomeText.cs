using Assets.Scripts.Workers.IO.Data_Entities;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class AwesomeText : MonoBehaviour
    {
        [SerializeField]
        private ScoreWord[] Words;

        Animator animator;
        Text text;
        Image image;

        public void Awake()
        {
            ScoreKeeper.PointsAwarded += ShowText;
            MenuProvider.MenuDisplayed += MenuProvider_MenuDisplayed;

            animator = GetComponent<Animator>();
            text = GetComponentInChildren<Text>();
            image = GetComponent<Image>();

            HideText();
        }

        private void MenuProvider_MenuDisplayed(System.Type type)
        {
            HideText();
        }

        public void OnDestroy()
        {
            ScoreKeeper.PointsAwarded -= ShowText;
            MenuProvider.MenuDisplayed -= MenuProvider_MenuDisplayed;
        }

        private void ShowText(int points, ISquarePiece[] pieces)
        {
            if (MenuProvider.Instance.OnDisplay)
            {
                HideText();
                return;
            }

            var key = ScoreWord.GetScoreThreshold(points);
            if (key == ScoreThreshold.None)
            {
                return;
            }

            SetEnabled(true);

            var list = Words.FirstOrDefault(x => x.ScoreThreshold == key).Words;

            text.text = list[Random.Range(0, list.Length)];
            animator.SetTrigger("Show");

            PlayScoreShotParticleEffect(key);
        }

        private static void PlayScoreShotParticleEffect(ScoreThreshold key)
        {
            if (key == ScoreThreshold.Medium)
            {
                GameResources.PlayEffect("Score Shot 1", Vector3.zero);
            }
            else if (key == ScoreThreshold.High)
            {
                GameResources.PlayEffect("Score Shot 2", Vector3.zero);
            }
            else if (key == ScoreThreshold.VeryHigh)
            {
                GameResources.PlayEffect("Score Shot 3", Vector3.zero);
            }
        }

        public void HideText()
        {
            SetEnabled(false);
        }

        private void SetEnabled(bool enabled)
        {            
            image.enabled = enabled;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(enabled);
            }
            animator.enabled = enabled;
        }
    }
}
