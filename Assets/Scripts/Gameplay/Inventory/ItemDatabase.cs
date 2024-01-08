using UnityEngine;
using WerewolfHunt.Inventory.Items;

namespace WerewolfHunt.Inventory
{
    [CreateAssetMenu(fileName = "New Item Database", menuName = "Werewolf Hunt/Inventory/Item Database")]
    public class ItemDatabase : ScriptableObject
    {
        [Header("Key Items")]
        public KeyItem hammer;
        public KeyItem table;
        public KeyItem hotDog;
        public KeyItem poster;
        public KeyItem textbook;
        public KeyItem machinePart;
        public KeyItem documentation;
        public KeyItem metalDetector;
        public KeyItem lightBulb;
        public KeyItem treasure;
        public KeyItem grenade;
        public KeyItem sugar;
        public KeyItem cream;
        public KeyItem flavoring;
    }
}