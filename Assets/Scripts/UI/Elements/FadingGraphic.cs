using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace WerewolfHunt.UI.Elements
{
    public class FadingGraphic : MonoBehaviour
    {
        [SerializeField] float inTime;
        [SerializeField] float outTime;

        private WaitForSecondsRealtime inWait;
        private WaitForSecondsRealtime outWait;
        private WaitForEndOfFrame startWait;

        [SerializeField] private Graphic[] graphics;

        private void Start()
        {
            ComputeWaits();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            ComputeWaits();
        }
#endif

        void ComputeWaits()
        {
            startWait = new WaitForEndOfFrame();
            inWait = new WaitForSecondsRealtime(inTime);
            if (inTime == outTime)
            {
                outWait = inWait;
            }
            else
            {
                outWait = new WaitForSecondsRealtime(outTime);
            }
        }
        private IEnumerator FadeOutCoroutine()
        {
            for (int i = 0; i < graphics.Length; i++)
            {
                graphics[i].CrossFadeAlpha(0, outTime, true);
            }
            yield return outWait;
            gameObject.SetActive(false);
        }

        private IEnumerator FadeInCoroutine()
        {
            for (int i = 0; i < graphics.Length; i++)
            {
                graphics[i].gameObject.SetActive(true);
                graphics[i].canvasRenderer.SetAlpha(0);
            }
            yield return startWait;
            for (int i = 0; i < graphics.Length; i++)
            {
                graphics[i].CrossFadeAlpha(1, inTime, true);
            }
            yield return inWait;
        }

        public void FadeIn()
        {
            gameObject.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(FadeInCoroutine());
        }

        public void FadeOut()
        {
            for (int i = 0; i < graphics.Length; i++)
            {
                graphics[i].canvasRenderer.SetAlpha(1);
            }
            gameObject.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(FadeOutCoroutine());
        }
    }
}
