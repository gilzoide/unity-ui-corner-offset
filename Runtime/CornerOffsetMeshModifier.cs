using UnityEngine;

namespace Gilzoide.UiCornerOffset
{
    public class CornerOffsetMeshModifier : ACornerOffsetMeshModifier
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

        protected override Vector3 GetOffsetForRectPosition(Rect rect, Vector2 position)
        {
            Vector2 normalizedPosition = Rect.PointToNormalized(rect, position);
            Vector2 offset = BottomLeft * (1f - normalizedPosition.x) * (1f - normalizedPosition.y)
                + TopLeft * (1f - normalizedPosition.x) * normalizedPosition.y
                + TopRight * normalizedPosition.x * normalizedPosition.y
                + BottomRight * normalizedPosition.x * (1f - normalizedPosition.y);
            if (NormalizedOffset)
            {
                offset *= rect.size;
            }
            return offset;
        }
    }
}
