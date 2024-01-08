using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WerewolfHunt.UI.Elements;

namespace WerewolfHunt.UI
{
    public class VideoSettingsViewModel : MonoBehaviour
    {
        [SerializeField] private OptionSelect resolution;
        [SerializeField] private OptionSelect framerate;
        [SerializeField] private OptionSelect displayMode;

        private List<Resolution> supportedResolutions;

        private void OnEnable()
        {
            UpdateResolutions();
            UpdateFramerate();
        }

        private void UpdateResolutions()
        {
            supportedResolutions = Screen.resolutions.ToList();
            for (int i = supportedResolutions.Count - 2; i >= 0; i--)
            {
                if (DoResolutionsMatch(supportedResolutions[i], supportedResolutions[i + 1]))
                {
                    supportedResolutions.RemoveAt(i + 1);
                }
            }

            string[] options = new string[supportedResolutions.Count];
            int selectedIndex = 0;

            for (int i = 0; i < supportedResolutions.Count; i++)
            {
                if (supportedResolutions[i].width == Screen.currentResolution.width && supportedResolutions[i].height == Screen.currentResolution.height)
                {
                    selectedIndex = i;
                }
                options[i] = supportedResolutions[i].width + "x" + supportedResolutions[i].height;
            }

            resolution.SetOptions(options, selectedIndex);

            displayMode.SetSelectedIndex(Screen.fullScreen ? 0 : 1);
        }

        public void ApplyChanges()
        {
            bool fullscreen = displayMode.selectedIndex == 0;
            SettingsManager.Instance.SetResolution(supportedResolutions[resolution.selectedIndex], fullscreen);

            if (framerate.selectedIndex == 2)
            {
                SettingsManager.Instance.SetVSync(1);
            }
            else
            {
                SettingsManager.Instance.SetFramerate(framerate.selectedIndex == 0 ? 30 : 60);
                SettingsManager.Instance.SetVSync(0);
            }

            SettingsManager.Instance.SaveSettings();
        }

        private bool DoResolutionsMatch(Resolution resolution1, Resolution resolution2)
        {
            return resolution1.width == resolution2.width && resolution2.height == resolution2.height;
        }

        private void UpdateFramerate()
        {
            if (QualitySettings.vSyncCount > 0)
            {
                framerate.SetSelectedIndex(2);
            }
            else if (Application.targetFrameRate == 60)
            {
                framerate.SetSelectedIndex(1);
            }
            else
            {
                framerate.SetSelectedIndex(0);
            }
        }
    }
}
