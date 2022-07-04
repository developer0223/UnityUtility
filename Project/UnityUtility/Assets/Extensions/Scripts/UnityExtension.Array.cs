/// <summary>
/// Extension class for Array<T>
/// </summary>
public static partial class UnityExtension
{
    public static bool ContainsIndex<T>(this T[] thiz, int index)
    {
        return thiz.Length > 1 && index >= 0 && index <= thiz.Length - 1;
    }

    public static T Get<T>(this T[] thiz, int index)
    {
        if (thiz.ContainsIndex<T>(index))
        {
            return thiz[index];
        }

        return default;
    }

    public static bool TryGetValue<T>(this T[] thiz, int index, out T value)
    {
        if (thiz.ContainsIndex(index))
        {
            value = thiz.Get(index);
            return true;
        }

        value = default;
        return false;
    }

    public static T First<T>(this T[] thiz)
    {
        return thiz.Get(0);
    }

    public static T Last<T>(this T[] thiz)
    {
        return thiz.Get(thiz.Length - 1);
    }
}