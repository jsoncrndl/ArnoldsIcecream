using Unity.VisualScripting;
using UnityEngine;
using WerewolfHunt.Manager;
using WerewolfHunt.Player;

namespace WerewolfHunt.VisualScripting
{
    public class StartDialogue : Unit
    {
        [DoNotSerialize]
        [PortLabelHidden]
        public ControlInput inputTrigger;

        [DoNotSerialize]
        [PortLabelHidden]
        public ControlOutput outputTrigger;

        [DoNotSerialize]
        public ValueInput playerController;

        [DoNotSerialize]
        public ValueOutput playerOut;

        protected override void Definition()
        {
            playerController = ValueInput<PlayerController>("player");

            playerOut = ValueOutput("player", (flow) => flow.GetValue(playerController));

            outputTrigger = ControlOutput("output");
            inputTrigger = ControlInput("input", (flow) =>
            {
                CursorManager.Instance.setCursor();
                flow.GetValue<PlayerController>(playerController).SetNextState(PlayerController.State.DIALOGUE, true);
                Time.timeScale = 0;
                return outputTrigger;
            });

            Succession(inputTrigger, outputTrigger);
        }
    }
}
