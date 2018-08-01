using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISL_Net
{
    public class NetworkManager : ISL_Helper.ISL_Instance<NetworkManager>
    {
        ISL_Connection _netConnection;
        ulong curBatchID;

        //Todo 异步防止冲突机制

        List<MsgBase> _sendList;
        List<MsgBase> _revList;

        Dictionary<ulong,KeyValuePair<ISL_Event.NetSendCallBack,float>> _msgWaitingDic;
        Dictionary<uint, ISL_Event.NetSendCallBack> _msgListenerDic;

        public void Init()
        {
            //Todo :根据配置

            string ip = "127.0.0.1";
            ushort port = 6666;
            _netConnection = new ISL_Connection();
            _netConnection.CreateConnection(ip, port);

            _sendList = new List<MsgBase>();
            _revList = new List<MsgBase>();
            curBatchID = 0;
            _msgListenerDic = new Dictionary<uint, ISL_Event.NetSendCallBack>();
            _msgWaitingDic = new Dictionary<ulong, KeyValuePair<ISL_Event.NetSendCallBack, float>>();

            _netConnection.StartMsgRecieve();

        }

        public void Tick()
        {

            //Todo 解决异步冲突问题
            //处理接受到的消息           
            foreach(MsgBase msg in _revList)
            {
                //按类型
                uint msgtype = msg.msgType;
                if(_msgListenerDic.ContainsKey(msgtype))
                {
                    _msgListenerDic[msgtype].Invoke(msg);
                }

                //按批次
                ulong batchID = msg.msgBatchID;
                if(_msgWaitingDic.ContainsKey(batchID))
                {
                    _msgWaitingDic[batchID].Key.Invoke(msg);
                    _msgWaitingDic.Remove(batchID);
                }
            }
            _revList.Clear();

            //还在等待的事件
            List<ulong> batchList = _msgWaitingDic.Keys.ToList(); 
            foreach(var batchId in batchList)
            {
                float leftTime = _msgWaitingDic[batchId].Value - UnityEngine.Time.deltaTime;
                if(leftTime>0)
                {
                    _msgWaitingDic[batchId] = new KeyValuePair<ISL_Event.NetSendCallBack, float>
                                                            (_msgWaitingDic[batchId].Key, leftTime);
                }
                else
                {
                    //Todo 处理超时无响应

                    _msgWaitingDic.Remove(batchId);
                }

            }


            //执行发送消息
            if (_sendList.Count > 0 ) _netConnection.StartMsgSend();
        }


        //发送消息并监听这个批次返回的消息
        public void PushMsg(MsgBase msg,ISL_Event.NetSendCallBack callback = null,float waitTime = 5)
        {
            _sendList.Add(msg);
            msg.msgBatchID = ++curBatchID;

            if(callback != null)
            {
                if (!_msgWaitingDic.ContainsKey(curBatchID))
                {
                    _msgWaitingDic.Add(curBatchID, new KeyValuePair<ISL_Event.NetSendCallBack, float>(callback,waitTime));
                }
                else
                {
                    //报错批次有问题？


                }
            }
        }


        //对某种类型的消息挂载监听
        public void BindMsg(uint msg_type, ISL_Event.NetSendCallBack callback)
        {
            if(!_msgListenerDic.ContainsKey(msg_type))
            {
                _msgListenerDic.Add(msg_type, callback);
            }
            else
            {
                _msgListenerDic[msg_type] += callback;
            }
        }


        //连接层 接收消息 加入列表
        public void AddRevMsg(MsgBase msg)
        {
            //Todo线程安全

            _revList.Add(msg);
        }

        public MsgBase GetOneSendMsg()
        {
            MsgBase msg = null;
            //todo 线程安全
            if (_sendList.Count > 0)
            {
                msg = _sendList[0];
                _sendList.RemoveAt(0);
            }

            return msg;
        }
    }
}
