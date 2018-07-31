using UnityEngine;
using System.Collections;
using System.Text;

//这个类用于实现高级log功能
//拓展富文本：http://www.ceeger.com/Manual/StyledText.html
//开关Log功能
public static class SGG_Logger
{
    //Log分类
    public class LogTypeInfo
    {
        //标签
        public string title;
        //颜色
        public Color contentColor;
        public Color titleColor;
        //这个类型的开关
        public bool bLogEnable;

        public LogTypeInfo(string t, Color tc, Color cc, bool bOpen = true)
        {
            title = t;
            contentColor = cc;
            titleColor = tc;
            bLogEnable = bOpen;
        }
    }

    // 定义的Log标签区域   
    //public static LogTypeInfo FileServer = new LogTypeInfo("版本文件服务器", Color.red, Color.white, true);
    //public static LogTypeInfo CSV = new LogTypeInfo("CSV", Color.red, Color.white, true);
    //public static LogTypeInfo INI = new LogTypeInfo("INI", Color.red, Color.white, true);

    //public static LogTypeInfo TOOL = new LogTypeInfo("工具", Color.cyan, Color.white, true);
    //Log总开关
    public static bool bLogRecord = true;


    //Log调用
    public static void Print(LogTypeInfo typeInfo, string content)
    {
        //总开关控制  &&   分类开关
        if (bLogRecord && typeInfo.bLogEnable)
        {
            logOut(typeInfo, content);
        }

    }


    private static void logOut(LogTypeInfo typeInfo, string content)
    {

        StringBuilder sb = new StringBuilder();
        sb.Append("[");
        sb.Append(typeInfo.title, typeInfo.titleColor);
        sb.Append("]");
        sb.Append(content, typeInfo.contentColor);

        Debug.Log(sb.ToString());
    }
}