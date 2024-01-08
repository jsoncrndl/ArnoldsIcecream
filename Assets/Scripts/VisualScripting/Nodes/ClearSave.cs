using Unity.VisualScripting;
using WerewolfHunt.Manager;

namespace WerewolfHunt.VisualScripting
{
    public class ClearSave : Unit
    {
        [DoNotSerialize]
        [PortLabelHidden]
        public ControlInput inputTrigger;

        [DoNotSerialize]
        [PortLabelHidden]
        public ControlOutput outputTrigger;

        protected override void Definition()
        {
            outputTrigger = ControlOutput(nameof(outputTrigger));

            inputTrigger = ControlInput(nameof(inputTrigger), (flow) =>
            {
                PersistenceManager.Instance.ClearSaves();
                return outputTrigger;
            });

            Succession(inputTrigger, outputTrigger);
        }
    }
}
