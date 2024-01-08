using Unity.VisualScripting;
using UnityEngine;
using WerewolfHunt.Manager;
using WerewolfHunt.Mechanics;

namespace WerewolfHunt.VisualScripting
{
    public class Warp : Unit
    {
        [DoNotSerialize]
        [PortLabelHidden]
        public ControlInput inputTrigger;

        [DoNotSerialize]
        [PortLabelHidden]
        public ControlOutput outputTrigger;

        [DoNotSerialize]
        [PortLabelHidden]
        public ValueInput warp;

        protected override void Definition()
        {
            warp = ValueInput<StartLocation>("warp", null);
            outputTrigger = ControlOutput(nameof(outputTrigger));

            inputTrigger = ControlInput(nameof(inputTrigger), (flow) =>
            {
                SceneWarper.Instance.Warp(flow.GetValue<StartLocation>(warp));
                return outputTrigger;
            });

            Succession(inputTrigger, outputTrigger);
        }
    }
}
