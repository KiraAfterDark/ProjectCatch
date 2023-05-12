using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace ProjectCatch.Ui.Tabs
{
    public class TabButton<T> : MonoBehaviour where T : Enum
    {
        private bool canSelect = false;
        
        private T value;
        private Action<T> callback;

        [Title("Text")]

        [SerializeField]
        private TextMeshProUGUI text;
        
        public void Initialize(T value, Action<T> callback)
        {
            this.value = value;
            this.callback = callback;

            text.text = this.value.ToString();
        
            canSelect = true;
        }
        
        public void Select()
        {
            if (!canSelect)
            {
                return;
            }
        
            canSelect = false;
        
            callback?.Invoke(value);
        }
    }
}
