using UnityEngine;

namespace WerewolfHunt.Mechanics
{
    [ExecuteAlways]
    public class StartLocationMarker : MonoBehaviour
    {
        [SerializeField] private StartLocation location;
        public StartLocation startLocation => location;

        private void Awake()
        {
            location.sceneName = gameObject.scene.name;
            location.warpPos = transform.position;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.DrawIcon(transform.position, "Flag");
        }
#endif

    }
}
