using Unity.VisualScripting;
using UnityEngine;
using WerewolfHunt.Manager;
using WerewolfHunt.Player;

namespace WerewolfHunt.VisualScripting
{
    public class EndDialogue : Unit
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
            playerOut = ValueOutput("player", (flow) => flow.GetValue<PlayerController>(playerController));

            outputTrigger = ControlOutput("output");
            inputTrigger = ControlInput("input", (flow) =>
            {
                CursorManager.Instance.setCrosshair();
                flow.GetValue<PlayerController>(playerController).SetNextState(PlayerController.State.MOVE);
                Time.timeScale = 1;
                return outputTrigger;
            });
            Succession(inputTrigger, outputTrigger);
        }
    }
}
