using UnityEngine;
using UnityEngine.Events;

namespace WerewolfHunt.Mechanics
{
    public class LifecycleEvent : MonoBehaviour
    {
        public UnityEvent onStart;
        public UnityEvent onEnable;
        public UnityEvent onDisable;
        public UnityEvent onDestroy;

        private void Start()
        {
            onStart?.Invoke();
        }

        private void OnEnable()
        {
            onEnable?.Invoke();
        }

        private void OnDisable()
        {
            onDisable?.Invoke();
        }

        private void OnDestroy()
        {
            onDestroy?.Invoke();
        }
    }
}
