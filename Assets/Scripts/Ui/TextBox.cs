using TMPro;
using UnityEngine;

namespace ProjectCatch.Ui
{
    public class TextBox : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI text;

        public void Display(string display)
        {
            text.text = display;
        }

        public void Clear()
        {
            text.text = "";
        }
    }
}
