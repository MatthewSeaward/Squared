using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class UpNextSlot : MonoBehaviour
    {
        public void SetNextItem(Sprite sprite)
        {
            transform.GetChild(0).GetComponent<Image>().sprite = sprite;
        }
    }
}
