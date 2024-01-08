using UnityEngine;
using WerewolfHunt.Player;
using WerewolfHunt.UI.Elements;

namespace WerewolfHunt.UI
{
    public class MapController : MonoBehaviour
    {
        [SerializeField] private FadingGraphic map;
        bool isFaded;

        private void Start()
        {
            PlayerController.Instance.onOpenMap += ShowMap;
            ShowMap(false);
        }

        private void OnDestroy()
        {
            if (PlayerController.Instance != null)
            {
                PlayerController.Instance.onOpenMap -= ShowMap;
            }
        }

        public void ShowMap(bool open)
        {
            if (isFaded == open) return;

            if (open)
            {
                map.FadeIn();
            }
            else
            {
                map.FadeOut();
            }

            isFaded = open;
        }
    }
}
