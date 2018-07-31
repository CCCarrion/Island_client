using UnityEngine;
using System.Collections;
using System.Text;

public static class BuiltInClassExtend
{
    //StringBuilder 拓展方法加入富文本
    public static StringBuilder AppendLine(this StringBuilder strBuilder, string str, Color color)
    {
        string strColor = "<color=#" + ColorUtility.ToHtmlStringRGB(color) + ">" + str + "</color>";
        return strBuilder.AppendLine(strColor);
    }

    public static StringBuilder Append(this StringBuilder strBuilder, string str, Color color)
    {
        string[] lines = str.Split('\n');
        for (int i = 0;i < lines.Length - 1;i++)
        {
            strBuilder.AppendLine(lines[i], color);
        }
        string strColor = "<color=#" + ColorUtility.ToHtmlStringRGB(color) + ">" + lines[lines.Length - 1] + "</color>";
        return strBuilder.Append(strColor);
    }

    public static string ColorString(string str, Color color)
    {
        return "<color=#" + ColorUtility.ToHtmlStringRGB(color) + ">" + str + "</color>";
    }



}