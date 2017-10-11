using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MainScript : MonoBehaviour {
	const int listenPort = 5600;
	const int remotePort = 5601;
	const string remoteIp ="127.0.0.1";

	float wiiX = 1;
	float wiiY = 1;

	Osc handler;
	// Use this for initialization
	void Start () {
		UDPPacketIO udp = (UDPPacketIO)GetComponent("UDPPacketIO");
		udp.init(remoteIp, remotePort, listenPort);
		handler = (Osc)GetComponent("Osc");
		handler.init(udp);
		handler.SetAllMessageHandler(AllMessageHandler);
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3(wiiX*4, wiiY*-4, 1);
	}
	public void AllMessageHandler(OscMessage oscMessage) {
		string msgString = Osc.OscMessageToString(oscMessage); //the message and value combined
		string  msgAddress = oscMessage.Address; //the message parameters
		float msgValue0 = -((float)oscMessage.Values[0])*2+1; //the message value
		float msgValue1 = -((float)oscMessage.Values[1])*2+1; //the message value
		Debug.Log(msgString); //log the message and values coming from OSC

		switch (msgAddress) {
		case "/wii/irdata":
			wiiX = msgValue0;
			wiiY = msgValue1;
			break;
		default:
			break;
		}
	}
}
