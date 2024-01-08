using UnityEngine;

namespace WerewolfHunt.Manager
{
    public class CursorManager : MonoBehaviour
    {
        [SerializeField] private Texture2D cursorTexture;
        [SerializeField] private Texture2D crosshairTexture;
        public static CursorManager Instance { get; private set; }

        private Vector2 cursorHotspot;

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
            cursorHotspot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);
            setCursor();
        }
        public void setCrosshair()
        {
            Cursor.SetCursor(crosshairTexture, cursorHotspot, CursorMode.Auto);
        }

        public void setCursor()
        {
            Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);
        }
    }
}