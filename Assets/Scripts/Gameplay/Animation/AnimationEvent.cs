using UnityEngine;
using UnityEngine.Events;

namespace WerewolfHunt.Animation
{
    public class AnimationEvent : MonoBehaviour
    {
        public enum AnimationEventType { ATTACK_COMPLETE, ATTACK_SOUND, FOOTSTEP }
        private IAnimationEventListener[] listeners;
        [SerializeField] private UnityEvent unityEvent;

        private void Start()
        {
            listeners = GetComponentsInParent<IAnimationEventListener>();
        }

        public void RaiseEvent(AnimationEventType eventType)
        {
            foreach (IAnimationEventListener listener in listeners)
            {
                listener.ReceiveEvent(eventType);
            }
        }

        public void InvokeUnityEvent()
        {
            unityEvent.Invoke();
        }
    }
}