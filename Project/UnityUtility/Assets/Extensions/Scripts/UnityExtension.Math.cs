/// <summary>
/// Extension class for Math
/// </summary>
public static partial class UnityExtension
{
    public static bool IsBiggerThan(this float value, float other, bool excludeSame = false)
    {
        return excludeSame ? value >= other : value > other;
    }

    public static bool IsBiggerThan(this int value, int other, bool excludeSame = false)
    {
        return excludeSame ? value >= other : value > other;
    }



    public static bool IsSmallerThan(this float value, float other, bool excludeSame = false)
    {
        return excludeSame ? value <= other : value < other;
    }

    public static bool IsSmallerThan(this int value, int other, bool excludeSame = false)
    {
        return excludeSame ? value <= other : value < other;
    }
}