public static partial class UnityExtension
{
    public class TextColor
    {
        public static readonly string aqua = "#00ffff";
        public static readonly string black = "#000000";
        public static readonly string blue = "#0000ff";
        public static readonly string brown = "#a52a2a";
        public static readonly string cyan = "#00ffff";
        public static readonly string darkblue = "#0000a0";
        public static readonly string fuchsia = "#ff00ff";
        public static readonly string grey = "#808080";
        public static readonly string lightblue = "#add8e6";
        public static readonly string lime = "#00ff00";
        public static readonly string magenta = "#ff00ff";
        public static readonly string maroon = "#800000";
        public static readonly string navy = "#000080";
        public static readonly string olive = "#808000";
        public static readonly string orange = "#ffa500";
        public static readonly string purple = "#800080";
        public static readonly string red = "#ff0000";
        public static readonly string silver = "#c0c0c0";
        public static readonly string teal = "#008080";
        public static readonly string white = "#ffffff";
        public static readonly string yellow = "#ffff00";
    }

    public static string WithColor(this string content, string color)
    {
        return $"<color=" + color + ">" + content + "</color>";
    }

    public static string WithSize(this string content, int size)
    {
        return $"<size=" + size + ">" + content + "</size>";
    }

    public static string WithBold(this string content)
    {
        return $"<b>" + content + "</b>";
    }

    public static string WithItalic(this string content)
    {
        return $"<i>" + content + "</i>";
    }
}