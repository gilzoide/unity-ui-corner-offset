using UnityEngine;
using UnityEngine.UI;

namespace Gilzoide.GraphicCornerOffset
{
    [RequireComponent(typeof(RectTransform))]
    public class CornerOffsetMeshModifier : BaseMeshEffect
    {
        [Tooltip("Offset applied to the bottom left corner")]
        public Vector3 BottomLeft;
        [Tooltip("Offset applied to the top left corner")]
        public Vector3 TopLeft;
        [Tooltip("Offset applied to the top right corner")]
        public Vector3 TopRight;
        [Tooltip("Offset applied to the bottom right corner")]
        public Vector3 BottomRight;

        public override void ModifyMesh(VertexHelper vh)
        {
            Rect rect = ((RectTransform) transform).rect;
            
            UIVertex vertex = default;
            for (int i = 0; i < vh.currentVertCount; i++)
            {
                vh.PopulateUIVertex(ref vertex, i);

                Vector2 normalizedPosition = Rect.PointToNormalized(rect, vertex.position);

                vertex.position = vertex.position
                    + BottomLeft * (1f - normalizedPosition.x) * (1f - normalizedPosition.y)
                    + TopLeft * (1f - normalizedPosition.x) * normalizedPosition.y
                    + TopRight * normalizedPosition.x * normalizedPosition.y
                    + BottomRight * normalizedPosition.x * (1f - normalizedPosition.y);

                vh.SetUIVertex(vertex, i);
            }
        }
    }
}
