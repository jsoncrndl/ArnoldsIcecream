using System.Collections.Generic;
using UnityEngine;
using WerewolfHunt.Inventory.Items;

namespace WerewolfHunt.Inventory
{
    public abstract class RadialMenuOptions<T> : ScriptableObject where T : ScriptableObject
    {
        [SerializeField] private List<T> _options;
        public List<T> Options => _options;
    }
}
