namespace WerewolfHunt.Animation
{
    public interface IAnimationEventListener
    {
        public void ReceiveEvent(AnimationEvent.AnimationEventType eventType);
    }
}
