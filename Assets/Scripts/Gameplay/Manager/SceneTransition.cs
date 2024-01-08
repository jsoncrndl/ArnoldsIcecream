using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WerewolfHunt.Manager;


namespace WerewolfHunt.Manager
{
    public class SceneTransition : MonoBehaviour
    {
        public enum TransitionType { FADE, WIPE_CIRCLE }
        [SerializeField] private List<Image> fadeSprites;
        private Animator anim;
        public event Action onTransitionComplete;
        string animPrefix = "Fade";

        private void Awake()
        {
            anim = GetComponent<Animator>();
            fadeSprites[0].color = Color.black;
            SceneWarper.Instance.transitioner = this;
            onTransitionComplete += SceneWarper.Instance.ChangeScene;
        }

        private void OnDestroy()
        {
            onTransitionComplete -= SceneWarper.Instance.ChangeScene;
        }

        public void StartTransition(TransitionType type, Color color)
        {
            fadeSprites.ForEach(item => item.color = color);

            switch(type)
            {
                case TransitionType.FADE:
                    animPrefix = "Fade";
                    break;
                case TransitionType.WIPE_CIRCLE:
                    animPrefix = "WipeCircle";
                    break;
                default:
                    animPrefix = "Fade";
                    break;
            }

            anim.Play(animPrefix + "_Start");
        }

        public void TransitionComplete()
        {
            onTransitionComplete?.Invoke();
        }

        public void EndTransition()
        {
            anim.Play(animPrefix + "_Stop");
        }
    }
}
