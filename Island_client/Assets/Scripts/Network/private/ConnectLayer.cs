using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

using ISL_Data;

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
            IPAddress ipAdr = IPAddress.Parse("127.0.0.1");//ip地址
            IPEndPoint ipEp = new IPEndPoint(ipAdr, 6666);//ip地址和端口号
        }

        //连接服务器
        public bool ConnectToServer(string str)
        {
            sendSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            const int BUFFER_SISE = 1024;
            byte[] readBuff = new byte[BUFFER_SISE];
            sendSocket.Connect("127.0.0.1", 6666);

            byte[] bytes = System.Text.Encoding.Default.GetBytes(str);
            sendSocket.Send(bytes);
            int count = sendSocket.Receive(readBuff);
            str = System.Text.Encoding.UTF8.GetString(readBuff, 0, count);

            Debug.Log("Get From server :" + str);

            sendSocket.Close();
            return true;


        }


    }


    class ISL_Connection
    {
        byte[] _readBuff = new byte[GameSetting.ConstVar.NET_rev_buffer_size];
        Socket _socket;
        bool bRev;
        bool bSend;


        public ISL_Connection ()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public uint CreateConnection(string ip,ushort port)
        {

            _socket.Connect(ip, port);

            return GameSetting.ErrCodeDef.ISL_OK;
        }

        //开启异步读取消息
        public void StartMsgRecieve()
        {
            bRev = true;
            _socket.BeginReceive(_readBuff, 0, GameSetting.ConstVar.NET_rev_buffer_size, SocketFlags.None, AsyncRev, null);

        }

        void AsyncRev(IAsyncResult ar)
        {
            int revSize = _socket.EndReceive(ar);

            if (revSize > 0)
            {
                MsgBase msg = ProtoLayer.DecodeMsg(_readBuff);

                GameEntity.Instance.gNetManager.AddRevMsg(msg);
            }
            if (bRev == false) return;

            //继续等待消息
            StartMsgRecieve();
        }

        public void StartMsgSend()
        {
            if (bSend) return;  //正在发送

            MsgBase msg = GameEntity.Instance.gNetManager.GetOneSendMsg();
            if (msg == null)
            {
                bSend = false;
            }
            else
            {
                bSend = true;
                byte[] sendData = ProtoLayer.EncodeMsg(msg);

                _socket.BeginSend(sendData,0,sendData.Length, SocketFlags.None, AsyncSend, null);
            }
        }

        void AsyncSend(IAsyncResult ar)
        {
            _socket.EndSend(ar);
            bSend = false;
            //继续下一个
            StartMsgSend();
        }
        public uint CloseConnection()
        {
            bRev = false;
            _socket.Close();
            return GameSetting.ErrCodeDef.ISL_OK;
        }


        public uint SendMsg(byte[] sendData)
        {




            return GameSetting.ErrCodeDef.ISL_OK;
        }

        public uint RevMsg()
        {
            


            return GameSetting.ErrCodeDef.ISL_OK;
        }
    }
}
