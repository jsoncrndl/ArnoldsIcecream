using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.LookDev;
using WerewolfHunt.Player;
using WerewolfHunt.UI.Elements;

namespace WerewolfHunt.VisualScripting
{
    public class Say : WaitUnit
    {
        [Inspectable, Serialize]
        public string[] lines { get; set; }


        [DoNotSerialize]
        public ValueInput player;

        [DoNotSerialize]
        public ValueInput textbox;

        [DoNotSerialize]
        public ControlInput inputTrigger;

        [DoNotSerialize]
        public ControlOutput outputTrigger;

        [DoNotSerialize]
        public ValueOutput textboxOut;

        [DoNotSerialize]
        public ValueInput linesOverride;

        protected override void Definition()
        {
            base.Definition();

            linesOverride = ValueInput<string>("lineOverride", null);
            textbox = ValueInput<Textbox>("textbox", null);
            textboxOut = ValueOutput("textbox", (flow) => flow.GetValue<Textbox>(textbox));
        }

        protected override IEnumerator Await(Flow flow)
        {
            Textbox box = flow.GetValue<Textbox>(textbox);
            box.Open();



            string[] textLines;

            if (linesOverride == null || flow.GetValue<string>(linesOverride) == null)
            {
                textLines = lines;
            }
            else
            {
                textLines = new string[] { flow.GetValue<string>(linesOverride) };

            }

            yield return box.OpenDialogue(textLines);           

            box.Close();
            yield return exit;
        }
    }
}
