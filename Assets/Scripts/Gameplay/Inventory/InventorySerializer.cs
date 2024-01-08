using System.Collections.Generic;
using UnityEngine;

namespace WerewolfHunt.Inventory
{
    [CreateAssetMenu(fileName = "New Inventory Serializer", menuName = "Werewolf Hunt/Inventory/Inventory Serializer")]
    public class InventorySerializer : ScriptableObject
    {
        public List<Item> itemList = new List<Item>();

        private void Awake()
        {
            for (int i = 0; i <  itemList.Count; i++)
            {
                itemList[i].id = i;
            }
        }

        public int GetItemID(Item item)
        {
            return item.id;
        }

        public Item GetItemByID(int id)
        {
            return id >= itemList.Count ? null : itemList[id];
        }
    }
}