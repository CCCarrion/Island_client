using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHeartBeat : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameEntity.Instance.InitGame();
	}
	
	// Update is called once per frame
	void Update () {
        GameEntity.Instance.Tick();
	}
}
