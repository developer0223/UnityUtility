// Unity
using UnityEngine;

/// <summary>
/// Extension class for UnityEngine.Vector
/// </summary>
public static partial class UnityExtension
{
    #region Vector3
    public static Vector2 ToVector2(this Vector3 v) => new Vector2(v.x, v.y);

    public static Vector3 WithX(this Vector3 v, float x) => new Vector3(x, v.y, v.z);

    public static Vector3 WithY(this Vector3 v, float y) => new Vector3(v.x, y, v.z);

    public static Vector3 WithZ(this Vector3 v, float z) => new Vector3(v.x, v.y, z);
    #endregion

    #region Vector2
    public static Vector3 ToVector3(this Vector2 v) => new Vector3(v.x, v.y, 0);

    public static Vector2 WithX(this Vector2 v, float x) => new Vector2(x, v.y);

    public static Vector2 WithY(this Vector2 v, float y) => new Vector2(v.x, y);
    #endregion
}