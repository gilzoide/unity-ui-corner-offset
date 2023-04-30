# UI Corner Offset
Corner Offset mesh modifier for Unity UI, useful for shearing/skewing Graphics


## How to use
1. Add the [CornerOffsetMeshModifier](Runtime/CornerOffsetMeshModifier.cs) component to the Graphic object you want to modify.
   This works with all Image types, RawImage, legacy Text components and other custom graphics.
2. Set the offsets from each corner in the inspector.

   By default offsets are measured in units, but you can mark `Normalized Offset` to use normalized positions instead.
   This way 1 means 100% of the rectangle's width/height instead of 1 unit.
3. Enjoy 🍾