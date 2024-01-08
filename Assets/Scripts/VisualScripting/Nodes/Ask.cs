using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using WerewolfHunt.UI.Elements;

namespace HealersPath.VisualScripting
{
    [TypeIcon(typeof(IBranchUnit))]
    public class Ask : Unit
    {
        // Using L<KVP> instead of Dictionary to allow null key
        [DoNotSerialize]
        public List<ControlOutput> branches { get; private set; }
        [DoNotSerialize]
        public List<ValueInput> branchEnabled { get; private set; }

        [Inspectable, Serialize]
        public string question;

        [Inspectable, Serialize]
        public List<string> options { get; set; } = new List<string>();

        [Inspectable, Serialize]
        public bool conditionalResponses { get; set; } = false;
        /// <summary>
        /// The value on which to switch.
        /// </summary>
        [DoNotSerialize]
        [PortLabelHidden]
        public ValueInput textbox { get; private set; }

        [DoNotSerialize]
        [PortLabelHidden]
        public ValueInput questionOverride { get; private set; }

        public override bool canDefine => options != null;

        [DoNotSerialize]
        [PortLabelHidden]
        public ControlInput inputTrigger { get; private set; }

        protected override void Definition()
        {
            textbox = ValueInput<Textbox>(nameof(textbox));
            inputTrigger = ControlInputCoroutine(nameof(inputTrigger), Await);
            branches = new List<ControlOutput>();
            questionOverride = ValueInput<string>("question", null);

            foreach (var option in options)
            {
                var key = "%" + option;

                if (!controlOutputs.Contains(key))
                {
                    var branch = ControlOutput(key);
                    branches.Add(branch);

                    if (conditionalResponses)
                    {
                        var enabled = ValueInput(key, true);
                        branchEnabled.Add(enabled);

                    }

                    Succession(inputTrigger, branch);
                }
            }
        }

        protected IEnumerator Await(Flow flow)
        {
            Textbox box = flow.GetValue<Textbox>(textbox);
            box.Open();

            string questionText;

            if (questionOverride == null || flow.GetValue<string>(questionOverride) == null)
            {
                questionText = question; 
            }
            else
            {
                questionText = flow.GetValue<string>(questionOverride);
            }
            
            yield return box.Ask(questionText, options);
    
            yield return branches[box.getSelection()];
            box.Close();
        }
    }
}