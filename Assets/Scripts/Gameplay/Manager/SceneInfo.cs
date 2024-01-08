using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using WerewolfHunt.Mechanics;
using WerewolfHunt.Player;

namespace WerewolfHunt.Manager
{
    public class SceneInfo : MonoBehaviour
    {
        public static SceneInfo current;
        public Camera sceneCamera;
        [SerializeField] private StartLocation defaultLocation;
        public AudioClip defaultBackgroundMusic;

        private void Awake()
        {
            current = this;
        }


        void Start()
        {
            MusicPlayer.Instance.SetBackgroundMusic(defaultBackgroundMusic);
            Checkpoint start = SceneWarper.Instance.nextWarp;

            if (start.scene == "")
            {
                start = new Checkpoint { position = defaultLocation.warpPos, scene = defaultLocation.sceneName };
            }

            EventBus.Trigger("OnSceneLoaded", start);
        }
    }
}
