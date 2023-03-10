using UnityEngine;
using UnityEngine.UI;

namespace Gilzoide.GraphicCornerOffset
{
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
            Vector3 rectCenter = (transform is RectTransform rectTransform) ? rectTransform.rect.center : transform.position;
            
            UIVertex vertex = default;
            for (int i = 0; i < vh.currentVertCount; i++)
            {
                vh.PopulateUIVertex(ref vertex, i);
                Vector3 diffToCenter = vertex.position - rectCenter;
                if (diffToCenter.x < 0)
                {
                    if (diffToCenter.y < 0)
                    {
                        vertex.position += BottomLeft;
                    }
                    else
                    {
                        vertex.position += TopLeft;
                    }
                }
                else
                {
                    if (diffToCenter.y < 0)
                    {
                        vertex.position += BottomRight;
                    }
                    else
                    {
                        vertex.position += TopRight;
                    }
                }
                vh.SetUIVertex(vertex, i);
            }
        }
    }
}
