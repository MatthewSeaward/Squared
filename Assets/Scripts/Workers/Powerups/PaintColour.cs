using Assets.Scripts.Managers;
using Assets.Scripts.UI;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Piece_Selection;
using Assets.Scripts.Workers.Powerups.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Workers.Powerups
{
    class PaintColour : IPowerup
    {
        public Sprite Icon => GameResources.Sprites["Potion"];
        
        public string Name => "Paint Colour";

        public string Description => "Drag your finger over the board to make all pieces the same colour";

        public bool Enabled => true;

        private float TimeToPreformAction = 2f;

        public void Invoke()
        {
            PieceSelectionManager.Instance.PieceSelection = new PieceSelectionModePaintPieces();
            CountdownPanel.TimerElapsed += CountdownPanel_TimerElapsed;
            CountdownPanel.Instance.StartCountDown(TimeToPreformAction);
            GameManager.Instance.GamePaused = true;
        }

        private void CountdownPanel_TimerElapsed()
        {
            CountdownPanel.TimerElapsed -= CountdownPanel_TimerElapsed;
            GameManager.Instance.GamePaused = false;
            PieceSelectionManager.Instance.ReturnPieceSelectionModeToDefault();
        }

        public void Update(float deltaTime)
        {
        }

        public void MoveCompleted()
        {
        }
    }
}
