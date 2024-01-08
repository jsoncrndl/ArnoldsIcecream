using UnityEngine;
using UnityEngine.UI;

namespace WerewolfHunt.UI.Elements
{
    public class HeartList : MonoBehaviour
    {
        [SerializeField] private Sprite fullHeart;
        [SerializeField] private Sprite halfHeart;
        [SerializeField] private Sprite emptyHeart;

        [SerializeField] private Image[] hearts;

        private Animator[] animators;

        private void Awake()
        {
            animators = new Animator[hearts.Length];
            for (int i = 0; i < hearts.Length; i++)
            {
                animators[i] = hearts[i].GetComponent<Animator>();
            }
        }

        public void SetHealth(int currentHealth, int maxHealth)
        {
            int remainingHealth = currentHealth;
            for (int i = 0; i < hearts.Length; i++)
            {
                if (i >= maxHealth / 2)
                {
                    hearts[i].enabled = false;
                    continue;
                }

                hearts[i].enabled = true;
                Sprite newSprite;
                if (remainingHealth > 1)
                {
                    newSprite = fullHeart;
                    remainingHealth -= 2;
                }
                else if (remainingHealth == 1)
                {
                    newSprite = halfHeart;
                    remainingHealth -= 1;
                }
                else
                {
                    newSprite = emptyHeart;
                }

                if (hearts[i].sprite != newSprite && newSprite != fullHeart)
                {
                    animators[i].SetTrigger("Trigger");
                }

                hearts[i].sprite = newSprite;
            }
        }
    }
}
