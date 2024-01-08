using UnityEngine;

public class SpriteGizmo : MonoBehaviour
{
    [SerializeField] private string icon;
    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, icon);
    }
}
