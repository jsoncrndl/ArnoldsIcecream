using UnityEngine;
using WerewolfHunt.UI.Elements;

namespace WerewolfHunt.UI
{
    public class AccessibilitySettingsViewModel : MonoBehaviour
    {
        [SerializeField] private OptionSelect screenShake;
        [SerializeField] private OptionSelect hitFlash;

        private void OnEnable()
        {
            UpdateSettings();
        }

        private void UpdateSettings()
        {
            screenShake.SetSelectedIndex(GameSettings.ScreenShake ? 0 : 1);
            hitFlash.SetSelectedIndex(GameSettings.HitFlash ? 0 : 1);
        }

        public void Save()
        {
            SettingsManager.Instance.EnableHitFlash(hitFlash.selectedIndex == 0);
            SettingsManager.Instance.EnableCameraShake(screenShake.selectedIndex == 0);
            SettingsManager.Instance.SaveSettings();
        }
    }
}
