using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace WerewolfHunt.UI.Elements
{
    public class Textbox : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textArea;
        private FadingGraphic fadingGraphic;
        [SerializeField] private FadingGraphic buttonGraphic;

        private bool canContinue;
        private bool shouldContinue;
        private bool hasOption;
        private bool skip;

        private int selectedOption;

        private string[] linesToType;

        public UnityEvent onOpen;
        public UnityEvent onClose;
        public UnityEvent onContinue;

        private static WaitForSecondsRealtime slowDelay = new WaitForSecondsRealtime(.05f);
        private static WaitForSecondsRealtime normalDelay = new WaitForSecondsRealtime(.03f);
        private static WaitForSecondsRealtime fastDelay = new WaitForSecondsRealtime(.01f);
        private WaitUntil continueWait;
        private WaitUntil selectWait;

        public AudioClip clickSound;
        private AudioSource audioSource;

        private string[] wordBuffer = new string[35];

        [SerializeField] private List<TextMeshProUGUI> textOptions;

        private void Awake()
        {
            fadingGraphic = GetComponent<FadingGraphic>();
            continueWait = new WaitUntil(() => shouldContinue);
            selectWait = new WaitUntil(() => hasOption);

            audioSource = GetComponent<AudioSource>();
        }

        public IEnumerator OpenDialogue(string[] lines)
        {
            textArea.text = "";
            linesToType = lines;
            onOpen?.Invoke();

            yield return TypeLines();
        }

        public void Continue()
        {
            onContinue?.Invoke();
            if (canContinue)
            {
                shouldContinue = true;
            }
        }

        public void Skip()
        {
            if (!canContinue)
            {
                skip = true;
            }
        }

        public void Open()
        {
            onOpen?.Invoke();
            gameObject.SetActive(true);
        }

        public void Close()
        {
            onClose.Invoke();
            gameObject.SetActive(false);
        }

        public IEnumerator Ask(string question, List<string> options)
        {
            textArea.text = "";
            hasOption = false;

            yield return TypeLine(question);
            ShowOptions(options);

            yield return selectWait;
            HideOptions();
        }

        private void ShowOptions(List<string> options)
        {
            for (int i = 0; i < textOptions.Count; i++)
            {
                if (i >= options.Count)
                {
                    textOptions[i].gameObject.SetActive(false);
                    continue;
                }

                textOptions[i].gameObject.SetActive(true);
                textOptions[i].text = options[i];
            }
        }

        public void HideOptions()
        {
            for (int i = 0; i < textOptions.Count; i++)
            {
                textOptions[i].gameObject.SetActive(false);
            }
        }

        public void SelectOption(GameObject option)
        {
            for (int i = 0; i < textOptions.Count; i++)
            {
                if (!option.Equals(textOptions[i].gameObject)) continue;

                selectedOption = i;
                hasOption = true;
            }
        }

        private IEnumerator TypeLines()
        {
            for (int i = 0; i < linesToType.Length; i++)
            {
                canContinue = false;
                shouldContinue = false;

                linesToType[i] = linesToType[i].Replace("\\n", "\n");

                yield return TypeLine(linesToType[i]); 

                canContinue = true;
                buttonGraphic.gameObject.SetActive(true);
                yield return continueWait;
                buttonGraphic.gameObject.SetActive(false);
                textArea.text = "";
            }
        }

        private IEnumerator TypeLine(string line)
        {
            textArea.overflowMode = TextOverflowModes.Overflow;

            int wordStart = 0;
            int wordCount = 0;

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == ' ')
                {
                    wordBuffer[wordCount] = line.Substring(wordStart, i - wordStart + 1);
                    wordStart = i+1;
                    wordCount++;
                }
            }

            if (wordStart < line.Length)
            {
                wordBuffer[wordCount] = line.Substring(wordStart, line.Length - wordStart);
                wordCount++;
            }


            for (int i = 0; i < wordCount; i++)
            {
                bool newLine = false;

                float width = textArea.GetPreferredValues(textArea.text + wordBuffer[i]).x;

                if (width > textArea.rectTransform.rect.width)
                {
                    newLine = true;
                }

                if (newLine)
                {
                    textArea.text += "\n";
                }

                foreach (char c in wordBuffer[i])
                {
                    if (skip)
                    {
                        textArea.overflowMode = TextOverflowModes.Page;
                        textArea.text = line;
                        goto Loop_End;
                    }

                    textArea.text += c;
                    audioSource.PlayOneShot(clickSound);
                    yield return normalDelay;
                }

            }

            Loop_End:
            skip = false;
        }

        public int getSelection() => selectedOption;
    }
}
