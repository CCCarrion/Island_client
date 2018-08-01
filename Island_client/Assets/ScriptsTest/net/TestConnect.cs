using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ISL_Net;

public class TestConnect : MonoBehaviour {


    // Use this for initialization
    void Start () {
        GameEntity.Instance.gNetManager.BindMsg(1, SendCallBack);

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    [ContextMenu("点击发送")]
    void SendData()
    {
        TestMsg msg = new TestMsg();
        msg.msgType = 1;
        msg.msgContent = "Client";
        GameEntity.Instance.gNetManager.PushMsg(msg, SendCallBack);
    }


    public void SendCallBack(MsgBase msg)
    {
        Debug.Log((msg as TestMsg).msgContent);
    }


}
