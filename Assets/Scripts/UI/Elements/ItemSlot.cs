using UnityEngine;
using UnityEngine.UI;
using WerewolfHunt.Inventory;

namespace WerewolfHunt.UI.Elements
{
    public class ItemSlot : MonoBehaviour
    {
        [SerializeField] private Image itemImage;
        private int amount;

        public void SetItem(Item item)
        {
            if (item != null)
            {
                itemImage.sprite = item.inventorySprite;
                itemImage.enabled = true;
            }
            else
            {
                itemImage.enabled = false;
            }
        }
        public void SetItem(Sprite sprite)
        {
            if (sprite != null)
            {
                itemImage.sprite = sprite;
                itemImage.enabled = true;
            }
            else
            {
                itemImage.enabled = false;
            }
        }

        public void SetSlotAmount(int amount)
        {
            this.amount = amount;
        }
    }
}

