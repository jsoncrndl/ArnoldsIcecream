using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace WerewolfHunt.Manager
{
    public class MusicPlayer : MonoBehaviour
    {
        public static MusicPlayer Instance;

        [SerializeField] private float fadeTime;
        [SerializeField] private float fadeSmoothIterations = 10;
        [SerializeField] private float bgmVolume = -10;

        [SerializeField] private AudioMixer musicMixer;
        private AudioSource musicSource;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            musicSource = GetComponent<AudioSource>();
        }

        public void SetBackgroundMusic(AudioClip music)
        {
            if (music == musicSource.clip) return;
            StopAllCoroutines();
            StartCoroutine(FadeToMusic(music));
        }

        public void FadeOut()
        {
            StartCoroutine(Fade(true));
        }

        public void FadeIn()
        {
            StartCoroutine(Fade(false));
        }

        private IEnumerator Fade(bool isFadeOut)
        {
            float offset = isFadeOut ? 1 : 0;
            for (int i = 0; i <= fadeSmoothIterations; i++)
            {
                yield return new WaitForSecondsRealtime(fadeTime / fadeSmoothIterations);
                float t = i / fadeSmoothIterations;
                musicMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, bgmVolume, Mathf.Abs(offset-t)));
            }

            if (isFadeOut) musicSource.Stop();
        }

        private IEnumerator FadeToMusic(AudioClip music)
        {
            yield return Fade(true);
            musicSource.clip = music;
            musicMixer.SetFloat("MusicVolume", bgmVolume);
            musicSource.Play();
        }
    }
}
