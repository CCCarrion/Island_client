using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISL_Net
{
    public class NetworkManager : ISL_Helper.ISL_Instance<NetworkManager>
    {
        ISL_Connection _netConnection;



        public void Init()
        {
            //Todo :根据配置

            string ip = "127.0.0.1";
            ushort port = 6666;

            _netConnection.CreateConnection(ip, port);

        }

        public void Tick()
        {

        }
    }
}
