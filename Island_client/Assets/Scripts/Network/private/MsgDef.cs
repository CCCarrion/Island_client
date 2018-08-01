using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISL_Net
{
    public abstract class MsgBase
    {
        public uint msgType;   //类型
        public ulong msgBatchID;   //批号 根据批号做服务器返回对应

       // public abstract byte[] GetByteCode();
    }

    public class TestMsg : MsgBase
    {
        public string msgContent;     
        

    }

}
