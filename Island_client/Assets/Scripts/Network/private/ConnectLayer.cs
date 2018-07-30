using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace ISL_Net
{
    class ConnectLayer
    {
        Socket sendSocket;
        IPAddress ipAdr;
        IPEndPoint ipEp;

        //创建一个连接
        public ConnectLayer()
        {
            sendSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipAdr = IPAddress.Parse("127.0.0.1");//ip地址
            IPEndPoint ipEp = new IPEndPoint(ipAdr, 6666);//ip地址和端口号
        }

        //连接服务器
        public bool ConnectToServer()
        {
            const int BUFFER_SISE = 1024;
            byte[] readBuff = new byte[BUFFER_SISE];
            sendSocket.Connect("127.0.0.1", 6666);
            String str = "cat";
            byte[] bytes = System.Text.Encoding.Default.GetBytes(str);
            sendSocket.Send(bytes);
            int count = sendSocket.Receive(readBuff);
            str = System.Text.Encoding.UTF8.GetString(readBuff, 0, count);

            Debug.Log("Get From server :" + str);
           

            return true;


        }


    }
}
