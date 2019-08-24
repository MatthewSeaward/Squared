using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class AnimatedText : MonoBehaviour
    {      
        public void Show(string text)
        {
            Show(text, Color.white);
        }

        public void Show(string text, Color colour)
        {
            gameObject.SetActive(true);

            gameObject.GetComponentInChildren<TextMeshProUGUI>().text = text;
            gameObject.GetComponentInChildren<TextMeshProUGUI>().faceColor = colour;

            gameObject.GetComponentInChildren<Animator>().SetTrigger("Show");
        }

        public void HideText()
        {
            gameObject.SetActive(false);
        }
     
    }
}
