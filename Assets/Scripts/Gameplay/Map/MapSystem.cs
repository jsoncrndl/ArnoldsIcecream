using UnityEngine;

namespace WerewolfHunt.Map
{
    [ExecuteAlways]
    public class MapSystem : MonoBehaviour
    {
        [SerializeField] private Material mapItemMaterial;

        [SerializeField] private Transform mapBottomLeft;
        [SerializeField] private Transform mapTopRight;

        [SerializeField] private RenderTexture mapMask;

        public Vector2 offsetScale = Vector2.one;

        // Use this for initialization
        void Awake()
        {
            CalculateTransformationMatrix();
        }


#if UNITY_EDITOR
        private void OnValidate()
        {
            CalculateTransformationMatrix();      
        }
#endif

        void CalculateTransformationMatrix()
        {
            Vector2 scale = (new Vector2(mapMask.width, mapMask.height)) / (mapTopRight.position - mapBottomLeft.position);
            Vector2 offset = -(mapTopRight.position + mapBottomLeft.position) / 2;
            offset.x *= scale.x * offsetScale.x;
            offset.y *= scale.y * offsetScale.y;

            Matrix4x4 mapMatrix = Matrix4x4.identity;

            //World scale matrix
            mapMatrix.SetRow(0, new Vector4(scale.x, 0, 0, 0));
            mapMatrix.SetRow(1, new Vector4(0, scale.y, 0, 0));

            mapMatrix.SetColumn(3, offset);
            mapMatrix[3, 3] = 1; // Reset homogenous coordinate

            mapItemMaterial.SetMatrix("_MapTransformation", mapMatrix);

            Debug.Log(mapMatrix);
        }
    }
}