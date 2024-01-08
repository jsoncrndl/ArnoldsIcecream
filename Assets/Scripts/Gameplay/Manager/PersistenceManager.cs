using System;
using UnityEngine;
using WerewolfHunt.Inventory;
using WerewolfHunt.Inventory.Items;
using WerewolfHunt.Mechanics;
using WerewolfHunt.Player;

namespace WerewolfHunt.Manager
{
    public class PersistenceManager : MonoBehaviour
    {
        public static PersistenceManager Instance;
        public Inventory.Inventory inventory;
        public PlayerController.Stats stats;
        public Checkpoint checkpoint;

        [SerializeField] private InventorySerializer inventorySerializer;
        [SerializeField] private StartLocation gameStart;
        [SerializeField] private Inventory.Inventory startInventory;
        [SerializeField] private EquipmentList defaultEquipmentList;

        string defaultEquipment;
        string defaultCheckpoint;
        string defaultInventory;

        public bool hasStoredPlayer { get; private set; }

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
                return;
            }

            defaultEquipment = JsonUtility.ToJson(defaultEquipmentList);
            defaultCheckpoint = JsonUtility.ToJson(new Checkpoint(gameStart));
            defaultInventory = startInventory.Save();

            Load();
        }

        public void Save()
        {
            string inventoryJSON = inventory.Save();
            PlayerPrefs.SetString("inventory", inventoryJSON);

            string checkpointJSON = JsonUtility.ToJson(checkpoint);
            PlayerPrefs.SetString("checkpoint", checkpointJSON);
        }

        public void Load()
        {
            string inventoryJSON = PlayerPrefs.GetString("inventory");
            inventory.Load(inventoryJSON);

            string merchantInventoryJSON = PlayerPrefs.GetString("merchantInventory");

            string checkpointJSON = PlayerPrefs.GetString("checkpoint");
            JsonUtility.FromJson<Checkpoint>(checkpointJSON);
        }

        public void StorePlayer()
        {
            PlayerController player = PlayerController.Instance;

            if (player == null) return;

            hasStoredPlayer = true;
            stats = player.stats;
            inventory = player.inventory;
            checkpoint = player.checkpoint;
        }

        public void ClearSaves()
        {
            hasStoredPlayer = true;
            stats = new PlayerController.Stats();
            PlayerPrefs.SetString("inventory", defaultInventory);
            PlayerPrefs.SetString("checkpoint", defaultCheckpoint);
            PlayerPrefs.SetString("equipment", defaultEquipment);
        }

        [Serializable]
        private struct EquipmentList
        {
            public int item1;
            public int item2;
        }
    }
}
