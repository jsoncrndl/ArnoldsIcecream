using System;
using System.Collections.Generic;
using UnityEngine;

namespace WerewolfHunt.Inventory
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Werewolf Hunt/Inventory/Inventory")]
    public class Inventory : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] private InventorySerializer serializer;
        [SerializeField] private InventoryData inventory;

        public int AddItem(Item item, int amount)
        {
            //Check for an existing slot
            for (int i = 0; i < inventory.itemList.Count; i++)
            {
                InventoryStack slot = inventory.itemList[i];

                if (slot.item == item)
                {
                    int leftover = slot.AddAmount(amount);
                    if (slot.amount == 0)
                    {
                        inventory.itemList.RemoveAt(i);
                    }
                    return leftover;
                }
            }

            if (amount <= 0) return 0;

            inventory.itemList.Add(new InventoryStack(serializer, item));
            return inventory.itemList[inventory.itemList.Count - 1].AddAmount(amount);
        }

        public int GetAmount(Item item)
        {
            for (int i = 0; i < inventory.itemList.Count; i++)
            {
                InventoryStack slot = inventory.itemList[i];

                if (slot.item == item)
                {
                    return slot.amount;
                }
            }

            return 0;
        }

        public void Clear()
        {
            inventory.itemList.Clear();
        }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            for (int i = 0; i < inventory.itemList.Count; i++)
            {
                inventory.itemList[i].item = serializer.GetItemByID(inventory.itemList[i].ID);
            }
        }

        public string Save()
        {
            return JsonUtility.ToJson(inventory);
        }

        public void Load(string json)
        {
            inventory = JsonUtility.FromJson<InventoryData>(json);
        }

        public string PrintContents()
        {
            string contents = "";

            foreach (InventoryStack stack in inventory.itemList)
            {
                contents += stack.item + "x" + stack.amount + "\n"; 
            }
            return contents;
        }

        [Serializable]
        private struct InventoryData
        {
            public List<InventoryStack> itemList;
        }

        [Serializable]
        private class InventoryStack
        {
            public Item item;
            public int amount;
            public int ID;

            public InventoryStack(InventorySerializer serializer, Item item)
            {
                this.item = item;
                amount = 0;
                ID = serializer.GetItemID(item);
            }

            public int AddAmount(int amount)
            {
                this.amount += amount;
                return 0;
            }
        }
    }
}
