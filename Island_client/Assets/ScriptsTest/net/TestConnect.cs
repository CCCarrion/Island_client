using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ISL_Net;

public class TestConnect : MonoBehaviour {

    public string sendMSG;
    ConnectLayer con;
    // Use this for initialization
    void Start () {
        con = new ConnectLayer();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    [ContextMenu("点击发送")]
    void SendData()
    {
        con.ConnectToServer(sendMSG);
    }


}
