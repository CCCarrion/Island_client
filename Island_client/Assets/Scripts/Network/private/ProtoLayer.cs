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

        //Todo 改成protobuf
        //消息解码
        public static MsgBase DecodeMsg(byte[] revData)
        {
            string content = System.Text.Encoding.UTF8.GetString(revData, 0, revData.Length);

            string[] strArr = content.Split('|');

            TestMsg msg = new TestMsg();
            msg.msgBatchID = ulong.Parse(strArr[0]);
            msg.msgType = uint.Parse(strArr[1]);
            msg.msgContent = strArr[2];

            return msg;
        }

        //数据分发
        static void DispatchMsg()
        {



        }

        //Todo 改成protobuf
        //消息编码
        public static byte[] EncodeMsg(MsgBase msg)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(msg.msgBatchID);
            sb.Append("|");
            sb.Append(msg.msgType);
            sb.Append("|");
            sb.Append((msg as TestMsg).msgContent);
            byte[] bytes = System.Text.Encoding.Default.GetBytes(sb.ToString());
            return bytes;
        }
    }
}
