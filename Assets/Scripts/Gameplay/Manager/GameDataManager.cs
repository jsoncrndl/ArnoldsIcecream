using UnityEngine;
using WerewolfHunt.Inventory;

namespace WerewolfHunt.Manager
{
    internal class GameDataManager : MonoBehaviour
    {
        public static GameDataManager Instance;

        [SerializeField] private ItemDatabase items;
        public ItemDatabase itemDatabase => items;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
