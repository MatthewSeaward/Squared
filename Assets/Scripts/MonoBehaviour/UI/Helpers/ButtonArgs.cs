using UnityEngine.Events;

namespace Assets.Scripts.UI.Helpers
{
    public class ButtonArgs
    {
        public string Text { get; set; }
        public bool Enabled { get; set; } = true;
        public UnityAction Action { get; set; }
    }
}