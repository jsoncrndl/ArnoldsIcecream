using System;
using UnityEngine;

namespace WerewolfHunt
{
    [Serializable]
    public class GameSettings
    {
        public float sfxVolume;
        public float musicVolume;
        public float uiVolume;
        public bool mute;
        public Resolution resolution;
        public int framerate;
        public bool vsync;
        public bool screenShake;
        public bool hitFlash;
        public string controlBindings;
        public bool fullscreen;

        public static float SFXVolume => SettingsManager.Instance.settings.sfxVolume;
        public static float MusicVolume => SettingsManager.Instance.settings.musicVolume;
        public static float UIVolume => SettingsManager.Instance.settings.uiVolume;
        public static bool ScreenShake => SettingsManager.Instance.settings.screenShake;
        public static bool HitFlash => SettingsManager.Instance.settings.hitFlash;
    }
}
