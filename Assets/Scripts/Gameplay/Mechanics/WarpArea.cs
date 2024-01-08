using UnityEngine;
using WerewolfHunt.Manager;

namespace WerewolfHunt.Mechanics
{
    [RequireComponent(typeof(Collider2D))]
    public class WarpArea : MonoBehaviour
    {
        [SerializeField] private StartLocation warp;
        [SerializeField] private Color transitionColor = Color.black;
        //[SerializeField] private SceneTransition.TransitionType transitionType = SceneTransition.TransitionType.FADE;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Warp();
            }
        }

        public void Warp()
        {
            SceneWarper.Instance.Warp(warp);
        }


#if UNITY_EDITOR
        private void OnDrawGizmos()
        {

            Gizmos.DrawIcon(transform.position, "Home");
        }
#endif
    }
}