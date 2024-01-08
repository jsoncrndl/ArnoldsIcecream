using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(SpriteRenderer))]
public class DepthSortY : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float offset;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.sortingOrder = Mathf.FloorToInt(-(transform.position.y + offset) * 10);
    }
}