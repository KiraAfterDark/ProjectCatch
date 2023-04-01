using TMPro;
using UnityEngine;

namespace ProjectMaster.Battle.Ui
{
    public class BattleText : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI text;

        public void DisplayText(string display)
        {
            text.text = display;
        }

        public void Clear()
        {
            text.text = "";
        }
    }
}
