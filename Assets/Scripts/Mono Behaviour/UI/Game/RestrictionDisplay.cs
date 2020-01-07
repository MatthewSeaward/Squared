using Assets.Scripts.Managers;
using Assets.Scripts.Mono_Behaviour.UI.Components;
using Assets.Scripts.Workers.Generic_Interfaces;
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

        private IRestriction _restriction;
        private RestrictionInfo _restrictionInfo;
        private IUpdateable _updateableRestriction;

        private void Start()
        {
            PieceSelectionManager.SequenceCompleted += SequenceCompleted;

            var currentLevel = LevelManager.Instance.GetNextLevel();

            _restriction = GameManager.Instance.Restriction;
            _restriction.Reset();

            _updateableRestriction = _restriction as IUpdateable;

            _restrictionInfo = GetComponentInParent<RestrictionInfo>();

            UpdateRestriction(0);
        }

        private void OnDestroy()
        {
            PieceSelectionManager.SequenceCompleted -= SequenceCompleted;
        }

        private void SequenceCompleted(ISquarePiece[] pieces)
        {
            _restriction.SequenceCompleted(pieces);
        }

        private void Update()
        {
            UpdateRestriction(Time.deltaTime);        
        }

        private void UpdateRestriction(float deltaTime)
        {
            _updateableRestriction?.Update(deltaTime);

            _restrictionInfo.DisplayRestriction(_restriction);
            RestrictionText.color = _restriction.Ignored ? restrictionDisabledColour : Color.white;

            if (_restriction.ViolatedRestriction())
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
