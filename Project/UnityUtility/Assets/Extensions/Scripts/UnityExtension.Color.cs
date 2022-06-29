// Unity
using UnityEngine;

/// <summary>
/// Extension class for UnityEngine.Color
/// </summary>
public static partial class UnityExtension
{
    public static Color WithAlpha(this Color color, float alpha) => new Color(color.r, color.b, color.b, alpha);

    public static Color WithColor(this Color color, float r, float g, float b) => new Color(r, g, b, color.a);

    public static Color WithColor(this Color color, Color newColor) => new Color(newColor.r, newColor.g, newColor.b, color.a);
}