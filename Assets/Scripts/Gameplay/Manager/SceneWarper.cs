using UnityEngine;
using UnityEngine.SceneManagement;
using WerewolfHunt.Mechanics;

namespace WerewolfHunt.Manager
{
    public class SceneWarper : MonoBehaviour
    {
        public static SceneWarper Instance;

        public SceneTransition transitioner;
        public Checkpoint nextWarp;

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
        }

        public void Warp(StartLocation warp)
        {
            Warp(new Checkpoint() { position = warp.warpPos, scene = warp.sceneName });
        }

        public void Warp(Checkpoint warp)
        {
            //Fade out
            PersistenceManager.Instance.StorePlayer();
            nextWarp = warp;
            transitioner.StartTransition(SceneTransition.TransitionType.FADE, Color.black);
        }

        public void ChangeScene()
        {
            SceneManager.LoadScene(nextWarp.scene);
        }

        public void FinishLoad()
        {
            transitioner.EndTransition();
        }
    }
}