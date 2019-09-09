using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

            gameObject.GetComponentInChildren<Text>().text = text;
            gameObject.GetComponentInChildren<Text>().color = colour;

            gameObject.GetComponentInChildren<Animator>().SetTrigger("Show");
        }

        public void HideText()
        {
            gameObject.SetActive(false);
        }
     
    }
}
