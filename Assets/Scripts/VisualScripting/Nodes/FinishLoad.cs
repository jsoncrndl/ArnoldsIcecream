using Unity.VisualScripting;
using WerewolfHunt.Manager;

namespace WerewolfHunt.VisualScripting
{
    internal class FinishLoad : Unit
    {
        [PortLabelHidden]
        [DoNotSerialize]
        public ControlInput inputTrigger;

        [PortLabelHidden]
        [DoNotSerialize]
        public ControlOutput outputTrigger;

        protected override void Definition()
        {
            inputTrigger = ControlInput(nameof(inputTrigger), (flow) =>
            {
                SceneWarper.Instance.FinishLoad();

                return outputTrigger;
            });

            outputTrigger = ControlOutput(nameof(outputTrigger));
            Succession(inputTrigger, outputTrigger);
        }
    }
}
