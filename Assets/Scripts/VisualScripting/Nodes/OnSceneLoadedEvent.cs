using Unity.VisualScripting;
using WerewolfHunt.Mechanics;

namespace WerewolfHunt.VisualScripting
{
    public class OnSceneLoadedEvent : EventUnit<Checkpoint>
    {
        protected override bool register => true;
        
        [DoNotSerialize]// No need to serialize ports.
        public ValueOutput startLocation { get; private set; }// The Event output data to return when the Event is triggered.

        // Add an EventHook with the name of the Event to the list of Visual Scripting Events.
        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(ScriptingEvents.OnSceneLoaded);
        }

        protected override void Definition()
        {
            base.Definition();

            // Setting the value on our port.
            startLocation = ValueOutput<Checkpoint>("startLocation");
        }
        // Setting the value on our port.
        protected override void AssignArguments(Flow flow, Checkpoint start)
        {
            flow.SetValue(startLocation, start);
        }
    }
}
