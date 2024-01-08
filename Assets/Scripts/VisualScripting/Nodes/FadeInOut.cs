using Unity.VisualScripting;
using WerewolfHunt.Manager;

namespace WerewolfHunt.VisualScripting
{
    public class FadeOut : Unit
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
                SceneInfo.current.sceneCamera.GetComponent<CameraFunctions>().FadeOut();
                return outputTrigger;
            });
            Succession(inputTrigger, outputTrigger);
        }
    }

    public class FadeIn : Unit
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
                SceneInfo.current.sceneCamera.GetComponent<CameraFunctions>().FadeIn();
                return outputTrigger;
            });
            Succession(inputTrigger, outputTrigger);
        }
    }
}
