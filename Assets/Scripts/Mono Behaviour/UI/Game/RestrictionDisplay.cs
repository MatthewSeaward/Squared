using Assets.Scripts.Managers;
using Assets.Scripts.Mono_Behaviour.UI.Components;
using Assets.Scripts.Workers.Managers;
using Assets.Scripts.Workers.Score_and_Limits.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public delegate void RestrictionViolated();

    class RestrictionDisplay : MonoBehaviour
    {
        public static RestrictionViolated RestrictionViolated;

        [SerializeField]
        private Text RestrictionText;
        
        private static Color restrictionDisabledColour = new Color(0.7f, 0.7f, 0.7f, 0.7f);

        private IRestriction Restriction;
        private RestrictionInfo restrictionInfo;

        private void Start()
        {
            PieceSelectionManager.SequenceCompleted += SequenceCompleted;

            var currentLevel = LevelManager.Instance.GetNextLevel();

            Restriction = GameManager.Instance.Restriction;
            Restriction.Reset();

            restrictionInfo = GetComponentInParent<RestrictionInfo>();

            UpdateRestriction(0);
        }

        private void OnDestroy()
        {
            PieceSelectionManager.SequenceCompleted -= SequenceCompleted;
        }

        private void SequenceCompleted(ISquarePiece[] pieces)
        {
            Restriction.SequenceCompleted(pieces);
        }

        private void Update()
        {
            UpdateRestriction(Time.deltaTime);        
        }

        private void UpdateRestriction(float deltaTime)
        {
            Restriction.Update(deltaTime);

            restrictionInfo.DisplayRestriction(Restriction);
            RestrictionText.color = Restriction.Ignored ? restrictionDisabledColour : Color.white;

            if (Restriction.ViolatedRestriction())
            {
                RestrictionViolated?.Invoke();
            }
        }

        internal void ActivateRestriction()
        {
            RestrictionText.GetComponent<Animator>().SetTrigger("Activate");
        }
    }
}
