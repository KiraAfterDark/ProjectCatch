using System;
using System.Collections.Generic;
using Fsi.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectCatch.Gameplay.Items.Ui
{
    public class BagScreen : MbSingleton<BagScreen>
    {
        private Action<Item> itemRequestCallback;
        private Action closeCallback;
        
        private Inventory inventory;

        private readonly List<ItemButton> itemButtons = new List<ItemButton>();

        [Title("Canvas")]

        [SerializeField]
        private Canvas canvas;

        [Title("Prefabs")]

        [SerializeField]
        private ItemButton itemButtonPrefab;

        [Title("Scroll Views")]

        [SerializeField]
        private List<GameObject> itemTabGroup = new List<GameObject>();

        [Title("Containers")]

        [SerializeField]
        private Transform recoveryItemsContainer = new RectTransform();

        protected override void OnAwake()
        {
            canvas.gameObject.SetActive(false);
        }

        public void RequestItem(Inventory inventory, Action<Item> selectCallback, Action cancelCallback)
        {
            itemRequestCallback = selectCallback;
            closeCallback = cancelCallback;

            this.inventory = inventory;

            foreach(ItemButton button in itemButtons)
            {
                Destroy(button.gameObject);
            }

            itemButtons.Clear();

            foreach (KeyValuePair<RecoveryItem, int> recoveryItem in inventory.RecoveryItems)
            {
                ItemButton itemButton = Instantiate<ItemButton>(itemButtonPrefab, recoveryItemsContainer);
                itemButton.Initialize((Item)recoveryItem.Key, recoveryItem.Value, ItemSelected);

                itemButtons.Add(itemButton);
            }

            canvas.gameObject.SetActive(true);
            SwitchTab(0);
        }

        public void SwitchTab(int tab)
        {
            for (int i = 0; i < itemTabGroup.Count; i++)
            {
                if (i == tab)
                {
                    itemTabGroup[i].SetActive(true);
                }

                else
                {
                    itemTabGroup[i].SetActive(false);
                }
            }
        }

        private void ItemSelected(Item item)
        {
            Debug.Log($"Selected: {item}");

            itemRequestCallback?.Invoke(item);
            canvas.gameObject.SetActive(false);
        }

        public void CloseInventory()
        {
            closeCallback?.Invoke();
            canvas.gameObject.SetActive(false);
        }
    }
}
