using UnityEngine;

namespace WerewolfHunt.Inventory
{
    public class Item : ScriptableObject
    {
        public string itemName;
        public string displayName;
        public Sprite inventorySprite;
        [HideInInspector] public int id;
    }
}
