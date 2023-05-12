using System;
using ProjectCatch.Data.Attacks;
using TMPro;
using UnityEngine;

namespace ProjectCatch.Ui
{
    public class AttackButton : MonoBehaviour
    {
        private Attack attack;
        private Action<Attack> callback;

        private bool canSelect = false;
        
        [SerializeField]
        private TextMeshProUGUI text;

        [SerializeField]
        private AttackInfoBox hoverAttackInfo;

        public void Init(Attack attack, Action<Attack> callback)
        {
            canSelect = true;
            this.attack = attack;

            text.text = attack.Name;
            this.callback = callback;
            
            if (hoverAttackInfo)
            {
                hoverAttackInfo.gameObject.SetActive(false);
            }
        }

        public void OnSelected()
        {
            if (!canSelect)
            {
                return;
            }

            if (hoverAttackInfo)
            {
                hoverAttackInfo.gameObject.SetActive(false);
            }

            callback?.Invoke(attack);
        }

        public void OnHighlightStart()
        {
            if (hoverAttackInfo)
            {
                hoverAttackInfo.gameObject.SetActive(true);
                hoverAttackInfo.Show(attack);
            }
        }

        public void OnHighlightEnd()
        {
            if (hoverAttackInfo)
            {
                hoverAttackInfo.gameObject.SetActive(false);
            }
        }
    }
}
