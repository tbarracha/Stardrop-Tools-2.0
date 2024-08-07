﻿
namespace StardropTools.Tween
{
    public enum TweenType
    {
        None,

        // Values
        Float,
        Int,    
        Vector2,
        Vector3,
        Vector4,
        Quaternion,
        Color,
        ColorOpacity,

        // Shake / Punch Values
        ShakeFloat,
        ShakeInt,
        ShakeVector2,
        ShakeVector3,
        ShakeVector4,
        ShakeQuaternion,
        ShakeColor,
        ShakeColorOpacity,

        // Transform
        Position,
        LocalPosition,

        Rotation,
        LocalRotation,

        EulerRotation,
        LocalEulerRotation,

        LocalScale,

        // Shake / Punch Transform
        ShakePosition,
        ShakeLocalPosition,

        ShakeRotation,
        ShakeLocalRotation,

        ShakeEulerRotation,
        ShakeLocalEulerRotation,

        ShakeLocalScale,

        // Rect Transform
        AnchoredPosition,
        SizeDelta,
        RectSize,

        // Image
        ImageColor,
        ImageOpacity,
        ImagePixelsPerUnitMultiplier,

        // Text Mesh UGui
        TextMeshUGUIColor,
        TextMeshUGUIOpacity,
        TextMeshUGUISize,

        // Sprite renderer
        SpriteRendererColor,
        SpriteRendererOpacity,

        // Layout Element
        LayoutElement,

        // Line Renderer
        LineRendererColor,
        LineRendererOpacity,
    }
}