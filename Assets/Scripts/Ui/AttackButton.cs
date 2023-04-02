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

        public void Init(Attack attack, Action<Attack> callback)
        {
            canSelect = true;
            this.attack = attack;

            text.text = attack.Name;
            this.callback = callback;
        }

        public void OnSelected()
        {
            if (!canSelect)
            {
                return;
            }

            callback?.Invoke(attack);
        }
    }
}
