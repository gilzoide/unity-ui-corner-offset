using UnityEngine;
using UnityEngine.UI;

namespace Gilzoide.UiCornerOffset
{
    [RequireComponent(typeof(RectTransform))]
    public class SkewMeshModifier : ACornerOffsetMeshModifier
    {
        public enum Direction
        {
            Horizontal,
            Vertical,
        }

        [Tooltip("In which direction corners will be offset")]
        public Direction SkewDirection = Direction.Horizontal;

        [Tooltip("Angle in degrees that will be applied to skewed corners")]
        [Range(-90, 90)]
        public float Angle = 15;

        protected Vector2 _bottomLeft;
        protected Vector2 _topLeft;
        protected Vector2 _topRight;
        protected Vector2 _bottomRight;

        public override void ModifyMesh(VertexHelper vh)
        {
            if (!isActiveAndEnabled)
            {
                return;
            }

            RefreshCornerOffsets(RectTransform.rect.size);
            base.ModifyMesh(vh);
        }

        protected void RefreshCornerOffsets(Vector2 rectSize)
        {
            if (SkewDirection == Direction.Horizontal)
            {
                float xOffset = Mathf.Abs(Mathf.Tan(Mathf.Deg2Rad * Angle)) * rectSize.y;
                if (Angle > 0)
                {
                    _bottomLeft = _topRight = Vector2.zero;
                    _topLeft = new Vector2(xOffset, 0);
                    _bottomRight = new Vector2(-xOffset, 0);
                }
                else
                {
                    _topLeft = _bottomRight = Vector2.zero;
                    _bottomLeft = new Vector2(xOffset, 0);
                    _topRight = new Vector2(-xOffset, 0);
                }
            }
            else
            {
                float yOffset = Mathf.Abs(Mathf.Tan(Mathf.Deg2Rad * Angle)) * rectSize.x;
                if (Angle > 0)
                {
                    _bottomLeft = _topRight = Vector2.zero;
                    _topLeft = new Vector2(0, -yOffset);
                    _bottomRight = new Vector2(0, yOffset);
                }
                else
                {
                    _topLeft = _bottomRight = Vector2.zero;
                    _bottomLeft = new Vector2(0, yOffset);
                    _topRight = new Vector2(0, -yOffset);
                }
            }
        }

        protected override Vector3 GetOffsetForRectPosition(Rect rect, Vector2 position)
        {
            Vector2 normalizedPosition = Rect.PointToNormalized(rect, position);
            return _bottomLeft * (1f - normalizedPosition.x) * (1f - normalizedPosition.y)
                + _topLeft * (1f - normalizedPosition.x) * normalizedPosition.y
                + _topRight * normalizedPosition.x * normalizedPosition.y
                + _bottomRight * normalizedPosition.x * (1f - normalizedPosition.y);
        }
    }
}
