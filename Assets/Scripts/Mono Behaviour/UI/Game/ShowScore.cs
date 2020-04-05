using Assets.Scripts.Workers.Helpers;
using Assets.Scripts.Workers.Helpers.Extensions;
using Assets.Scripts.Workers.IO.Data_Entities;
using UnityEngine;

namespace Assets.Scripts
{
    public delegate void ShowScoreHidden();

    public class ShowScore : MonoBehaviour
    {
        public static ShowScore Instance;

        public static event ShowScoreHidden ShowScoreHidden;

        private int _countOfScoreOnShow;

        private int CountOfScoreOnShow
        {
            get
            {
                return _countOfScoreOnShow;
            }
            set
            {
                _countOfScoreOnShow = Mathf.Clamp(value, 0, int.MaxValue);

                if (_countOfScoreOnShow == 0)
                {
                    ShowScoreHidden?.Invoke();
                }
            }
        }

        public bool ScoreOnShow => CountOfScoreOnShow > 0;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            ScoreKeeper.PointsAwarded += ScoreKeeper_PointsAwarded;
        }

        private void OnDestroy()
        {
            ScoreKeeper.PointsAwarded -= ScoreKeeper_PointsAwarded;
        }

        private void ScoreKeeper_PointsAwarded(int points, ISquarePiece[] pieces)
        {
            var position = pieces[pieces.Length-1].transform.position;

            var texture = pieces[0].Sprite.texture;
            var colour = texture.GetTextureColour();

            Show(points, position, colour);
        }

        public void Show(int points, Vector3 spawnPosition, Color colour)
        {
            var position = RectTransformUtility.WorldToScreenPoint(Camera.main, spawnPosition);

            var gameObject = ObjectPool.Instantiate(GameResources.GameObjects["Animated Text"], position);

            gameObject.GetComponent<RectTransform>().SetParent(transform);
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            gameObject.transform.SetSiblingIndex(transform.childCount - 1);

            var aniamtedText = gameObject.GetComponent<AnimatedText>();
            aniamtedText.TextHidden += AniamtedText_TextHidden;
            aniamtedText.Show($"+{points}", colour);

            CountOfScoreOnShow++;
        }

        private void AniamtedText_TextHidden(AnimatedText animatedText)
        {
            animatedText.TextHidden -= AniamtedText_TextHidden;
            CountOfScoreOnShow --;
        }
    }
}