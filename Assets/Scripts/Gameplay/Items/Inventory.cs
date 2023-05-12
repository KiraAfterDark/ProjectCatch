using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace ProjectCatch.Gameplay.Items
{
    public class Inventory
    {
        public Dictionary<RecoveryItem, int> RecoveryItems { get; }

        public Inventory()
        {
            RecoveryItems = new Dictionary<RecoveryItem, int>();
        }

        public Inventory(SerializedDictionary<Item, int> inventory)
        {
            RecoveryItems = new Dictionary<RecoveryItem, int>();
            
            foreach (KeyValuePair<Item, int> entry in inventory)
            {
                AddItem(entry.Key, entry.Value);
            }
        }

        public bool AddItem(Item item, int amount = 1)
        {
            switch (item)
            {
                case RecoveryItem recoveryItem:
                    return AddRecoveryItem(recoveryItem, amount);
                
                default:
                    Debug.LogError($"Inventory does not support {item}.");
                    return false;
            }
        }

        private bool AddRecoveryItem(RecoveryItem recoveryItem, int amount = 1)
        {
            if (RecoveryItems.ContainsKey(recoveryItem))
            {
                RecoveryItems[recoveryItem] += amount;
            }
            else
            {
                RecoveryItems.Add(recoveryItem, amount);
            }

            return true;
        }
    }
}
