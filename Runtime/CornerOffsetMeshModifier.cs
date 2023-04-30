using UnityEngine;
using UnityEngine.UI;

namespace Gilzoide.UiCornerOffset
{
    [RequireComponent(typeof(RectTransform))]
    public class CornerOffsetMeshModifier : BaseMeshEffect
    {
        [Tooltip("Offset applied to the bottom left corner")]
        public Vector2 BottomLeft;
        [Tooltip("Offset applied to the top left corner")]
        public Vector2 TopLeft;
        [Tooltip("Offset applied to the top right corner")]
        public Vector2 TopRight;
        [Tooltip("Offset applied to the bottom right corner")]
        public Vector2 BottomRight;
        [Tooltip("If false, offsets use absolute position in units."
            + "\n\nIf true, offsets use normalized position from 0 to 1, where 1 means 100% of the width in X or 100% of the height in Y.")]
        public bool NormalizedOffset;

        public override void ModifyMesh(VertexHelper vh)
        {
            if (!isActiveAndEnabled)
            {
                return;
            }

            Rect rect = ((RectTransform) transform).rect;
            
            UIVertex vertex = default;
            for (int i = 0; i < vh.currentVertCount; i++)
            {
                vh.PopulateUIVertex(ref vertex, i);
                ApplyModifier(ref vertex.position, rect);
                vh.SetUIVertex(vertex, i);
            }
        }

        private void ApplyModifier(ref Vector3 position, Rect rect)
        {
            Vector3 offset = (Vector3) GetModifierValue(position, rect);
            if (NormalizedOffset)
            {
                offset *= rect.size;
            }
            position += offset;
        }

        private Vector2 GetModifierValue(Vector2 position, Rect rect)
        {
            Vector2 normalizedPosition = Rect.PointToNormalized(rect, position);
            return BottomLeft * (1f - normalizedPosition.x) * (1f - normalizedPosition.y)
                + TopLeft * (1f - normalizedPosition.x) * normalizedPosition.y
                + TopRight * normalizedPosition.x * normalizedPosition.y
                + BottomRight * normalizedPosition.x * (1f - normalizedPosition.y);
        }

#if UNITY_EDITOR
        private readonly Vector3[] _corners = new Vector3[4];
        private void OnDrawGizmosSelected()
        {
            if (!isActiveAndEnabled)
            {
                return;
            }

            ((RectTransform) transform).GetLocalCorners(_corners);

            Rect rect = ((RectTransform) transform).rect;
            ApplyModifier(ref _corners[0], rect);
            ApplyModifier(ref _corners[1], rect);
            ApplyModifier(ref _corners[2], rect);
            ApplyModifier(ref _corners[3], rect);

            Gizmos.DrawLine(transform.TransformPoint(_corners[0]), transform.TransformPoint(_corners[1]));
            Gizmos.DrawLine(transform.TransformPoint(_corners[1]), transform.TransformPoint(_corners[2]));
            Gizmos.DrawLine(transform.TransformPoint(_corners[2]), transform.TransformPoint(_corners[3]));
            Gizmos.DrawLine(transform.TransformPoint(_corners[3]), transform.TransformPoint(_corners[0]));
        }
#endif
    }
}
