using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISL_Net
{

    //协议封装解析
    class ProtoLayer
    {

        //处理数据入口
        public static void ProcessMsg(byte[] revData)
        {



        }

        //消息解码
        public static MsgBase DecodeMsg(byte[] revData)
        {

            return null;
        }

        //数据分发
        static void DispatchMsg()
        {



        }

        //消息编码
        public static byte[] EncodeMsg(MsgBase msg)
        {
            StringBuilder sb = new StringBuilder();

            byte[] bytes = System.Text.Encoding.Default.GetBytes(sb.ToString());
            return null;
        }
    }
}
