using UnityEngine;

public class SetScale : MonoBehaviour
{
    public void SetObjectScale(float scale)
    {
        transform.localScale = Vector3.one * scale;
    }
}
