using Unity.VisualScripting;
using UnityEngine;
using WerewolfHunt.Manager;

namespace WerewolfHunt.VisualScripting
{
    public class SetMusic : Unit
    {
        [DoNotSerialize]
        [PortLabelHidden]
        public ControlInput inputTrigger;

        [DoNotSerialize]
        [PortLabelHidden]
        public ControlOutput outputTrigger;

        [DoNotSerialize]
        [PortLabelHidden]
        public ValueInput music;
        

        protected override void Definition()
        {
            music = ValueInput<AudioClip>("music");
            outputTrigger = ControlOutput(nameof(outputTrigger));

            inputTrigger = ControlInput(nameof(inputTrigger), (flow) =>
            {
                MusicPlayer.Instance.SetBackgroundMusic(flow.GetValue<AudioClip>(music));
                return outputTrigger;
            });

            Succession(inputTrigger, outputTrigger);
        }
    }
}
