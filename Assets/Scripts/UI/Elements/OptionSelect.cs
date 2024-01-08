using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace WerewolfHunt.UI.Elements
{
    public class OptionSelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private string[] options;
        [SerializeField] private TextMeshProUGUI text;
        
        public int selectedIndex { get; private set; }
        public UnityEvent<int> selectedIndexChanged;

        public Image incrementButton;
        public Image decrementButton;

        public GameObject incrementButtonHolder;
        public GameObject decrementButtonHolder;

        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip hoverSound;
        [SerializeField] private bool repeating;


        private void Awake()
        {
        }

        public void SetOptions(string[] options)
        {
            this.options = options;
            selectedIndex = Mathf.Clamp(selectedIndex, 0, options.Length - 1);
            UpdateText();
        }

        public void SetOptions(string[] options, int selectedIndex)
        {
            this.options = options;
            SetSelectedIndex(selectedIndex);
        }

        public void SetSelectedIndex(int index)
        {
            if (repeating)
            {
                selectedIndex = (int)Mathf.Repeat(selectedIndex + 1, options.Length - 1);
            }

            selectedIndex = Mathf.Clamp(index, 0, options.Length - 1);

            incrementButton.gameObject.SetActive(true);
            incrementButton.gameObject.SetActive(true);

            if (!repeating && selectedIndex == options.Length - 1)
            {
                incrementButton.gameObject.SetActive(false);
            }
            if (!repeating && selectedIndex == 0)
            {
                decrementButton.gameObject.SetActive(false);
            }
            UpdateText();
        }

        public void Increment()
        {
            if (!repeating && selectedIndex >= options.Length - 1) return;

            selectedIndex = (int)Mathf.Repeat(selectedIndex + 1, options.Length);
            selectedIndexChanged.Invoke(selectedIndex);
            decrementButton.gameObject.SetActive(true);

            if (selectedIndex == options.Length - 1 && !repeating)
            {
                incrementButton.gameObject.SetActive(false);
            }

            UpdateText();
        }

        public void Decrement()
        {
            if (!repeating && selectedIndex <= 0) return;
            
            selectedIndex = (int)Mathf.Repeat(selectedIndex - 1, options.Length);
            selectedIndexChanged.Invoke(selectedIndex);
            incrementButton.gameObject.SetActive(true);


            if (selectedIndex == 0 && !repeating)
            {
                decrementButton.gameObject.SetActive(false);
            }

            UpdateText();
        }

        private void UpdateText()
        {
            text.text = options[selectedIndex];
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            incrementButtonHolder.SetActive(false);
            decrementButtonHolder.SetActive(false);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            audioSource.PlayOneShot(hoverSound);
            incrementButtonHolder.SetActive(true);
            decrementButtonHolder.SetActive(true);
        }
    }
}
