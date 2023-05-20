using UnityEngine;
using UnityEngine.UI;

namespace Gilzoide.UiCornerOffset
{
    [RequireComponent(typeof(RectTransform))]
    public abstract class ACornerOffsetMeshModifier : BaseMeshEffect
    {
        public RectTransform RectTransform => (RectTransform) transform;

        public override void ModifyMesh(VertexHelper vh)
        {
            if (!IsActive())
            {
                return;
            }

            Rect rect = RectTransform.rect;
            
            UIVertex vertex = default;
            for (int i = 0; i < vh.currentVertCount; i++)
            {
                vh.PopulateUIVertex(ref vertex, i);
                vertex.position += GetOffsetForRectPosition(rect, vertex.position);
                vh.SetUIVertex(vertex, i);
            }
        }

        /// <summary>Implement this in child classes for providing the offset, in units, for the given rect and local position.</summary>
        /// <param name="rect">Rectangle given by <see cref="RectTransform.rect"/>.</param>
        /// <param name="position">Local position. Most of the time this will be inside <paramref name="rect"/>.</param>
        protected abstract Vector3 GetOffsetForRectPosition(Rect rect, Vector2 position);

#if UNITY_EDITOR
        private readonly Vector3[] _corners = new Vector3[4];

        protected virtual void OnDrawGizmosSelected()
        {
            if (!IsActive())
            {
                return;
            }

            RectTransform.GetLocalCorners(_corners);
            Rect rect = RectTransform.rect;
            _corners[0] += GetOffsetForRectPosition(rect, _corners[0]);
            _corners[1] += GetOffsetForRectPosition(rect, _corners[1]);
            _corners[2] += GetOffsetForRectPosition(rect, _corners[2]);
            _corners[3] += GetOffsetForRectPosition(rect, _corners[3]);

            _corners[0] = transform.TransformPoint(_corners[0]);
            _corners[1] = transform.TransformPoint(_corners[1]);
            _corners[2] = transform.TransformPoint(_corners[2]);
            _corners[3] = transform.TransformPoint(_corners[3]);

            Gizmos.DrawLine(_corners[0], _corners[1]);
            Gizmos.DrawLine(_corners[1], _corners[2]);
            Gizmos.DrawLine(_corners[2], _corners[3]);
            Gizmos.DrawLine(_corners[3], _corners[0]);
        }
#endif
    }
}
