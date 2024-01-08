using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.Audio;

namespace WerewolfHunt
{

    public class SettingsManager : MonoBehaviour
    {
        public GameSettings settings { get; private set; }
        [SerializeField] private AudioMixer audioMixer;

        [SerializeField] private float defaultMusicVolume;
        [SerializeField] private float defaultSFXVolume;
        [SerializeField] private float defaultUIVolume;

        public static SettingsManager Instance { get; private set; }

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

            LoadSettings();
        }

        private void LoadSettings()
        {
            string settingsJSON = PlayerPrefs.GetString("settings", "");
            if (string.IsNullOrEmpty(settingsJSON))
            {
                LoadDefaultSettings();
                SaveSettings();
                return;
            }

            settings = JsonUtility.FromJson<GameSettings>(settingsJSON);
        }

        private void LoadDefaultSettings()
        {
            settings = new GameSettings();
            settings.resolution = Screen.currentResolution;
            settings.framerate = 60;
            settings.vsync = true;
            settings.mute = false;
            //audioMixer.GetFloat("MusicVolume", out settings.musicVolume);
            //audioMixer.GetFloat("SFXVolume", out settings.sfxVolume);
            //audioMixer.GetFloat("UIVolume", out settings.uiVolume);

            settings.musicVolume = defaultMusicVolume;
            settings.sfxVolume = defaultSFXVolume;
            settings.uiVolume = defaultUIVolume;

            settings.fullscreen = true;
            settings.hitFlash = true;
            settings.screenShake = true;
        }

        public void SaveSettings()
        {
            string settingsJSON = JsonUtility.ToJson(settings);
            PlayerPrefs.SetString("settings", settingsJSON);
            PlayerPrefs.Save();
        }

        public void SetMusicVolume(float volume)
        {
            settings.musicVolume = volume;
            audioMixer.SetFloat("MusicVolume", volume);
        }

        public void SetSFXVolume(float volume)
        {
            settings.sfxVolume = volume;
            audioMixer.SetFloat("SFXVolume", volume);
        }

        public void SetUIVolume(float volume)
        {
            settings.uiVolume = volume;
            audioMixer.SetFloat("UIVolume", volume);
        }

        public void Mute(bool mute)
        {
            audioMixer.SetFloat("MasterVolume", mute ? -80 : 0);
        }

        /// <summary>
        /// Set the target framerate for the application
        /// </summary>
        /// <param name="framerate">The framerate to target</param>
        public void SetFramerate(int framerate)
        {
            Application.targetFrameRate = framerate;
        }

        /// <summary>
        /// Sets the VSync count. 0 for off, 1 for full VSync. Must be between 0 and 4
        /// </summary>
        /// <param name="level"></param>
        public void SetVSync(int level)
        {
            if (level > 4 || level < 0) return;

            QualitySettings.vSyncCount = level;
            settings.vsync = level > 0;
        }

        public void SetResolution(Resolution resolution, bool fullscreen)
        {
            Screen.SetResolution(resolution.width, resolution.height, fullscreen);
            settings.resolution = resolution;
            settings.fullscreen = fullscreen;
        }

        public void EnableCameraShake(bool enable)
        {
            settings.screenShake = enable;
        }

        public void EnableHitFlash(bool enable)
        {
            settings.hitFlash = enable;
        }
    }
}