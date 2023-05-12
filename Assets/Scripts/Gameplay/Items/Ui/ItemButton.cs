using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectCatch.Gameplay.Items.Ui
{
    public class ItemButton : MonoBehaviour
    {
        private bool selectable = false;
        
        private Action<Item> callback;
        private Item item;

        [Title("References")]

        [SerializeField]
        private Image image;

        [SerializeField]
        private TextMeshProUGUI nameText;

        [SerializeField]
        private TextMeshProUGUI countText;

        public void Initialize(Item item, int count, Action<Item> callback)
        {
            image.sprite = item.Sprite;
            nameText.text = item.Name;
            countText.text = $"x{count}";

            this.callback = callback;
            selectable = true;
        }

        public void OnSelect()
        {
            if (!selectable)
            {
                return;
            }

            selectable = false;
            callback?.Invoke(item);
        }
    }
}
