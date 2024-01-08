using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(ParticleSystemRenderer))]
public class ParticleDepthSortY : MonoBehaviour
{
    private ParticleSystemRenderer particleRenderer;
    [SerializeField] private float offset;

    private void Awake()
    {
        particleRenderer = GetComponent<ParticleSystemRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        particleRenderer.sortingOrder = Mathf.FloorToInt(-(transform.position.y + offset) * 10);
    }
}