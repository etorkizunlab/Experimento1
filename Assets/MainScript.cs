using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MainScript : MonoBehaviour {
	const int listenPort = 5600;
	const int remotePort = 5601;
	const string remoteIp ="127.0.0.1";
	const int multiplierX = 8;
	const int multiplierY = -8;
	const int framesToReady = 1;
	const int framesActive = 10;

	float wiiX = 1;
	float wiiY = 1;
	int readyCount;
	int shownCount = 20;

	Diana d;

	Osc handler;
	// Use this for initialization
	void Start () {
		UDPPacketIO udp = (UDPPacketIO)GetComponent("UDPPacketIO");
		udp.init(remoteIp, remotePort, listenPort);
		handler = (Osc)GetComponent("Osc");
		handler.init(udp);
		handler.SetAllMessageHandler(AllMessageHandler);
		d = (Diana)(GameObject.Find("Diana").GetComponent("Diana"));
		gameObject.GetComponent<Renderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3(wiiX * multiplierX, wiiY * multiplierY, 1);
		if (shownCount > 0) gameObject.GetComponent<Renderer>().enabled = true;
		else gameObject.GetComponent<Renderer>().enabled = false;
		shownCount--;
	}

	void performShoot() {
		shownCount = framesActive;
		d.Shoot(wiiX * multiplierX, wiiY * multiplierY);
	}
	void tryShoot(float x, float y) {
		if (readyCount == 0) {
			wiiX = x;
			wiiY = y;
			performShoot();
		}
		readyCount = framesToReady;

	}
	public void AllMessageHandler(OscMessage oscMessage) {
		string msgString = Osc.OscMessageToString(oscMessage); //the message and value combined
		string  msgAddress = oscMessage.Address; //the message parameters
		float msgValue0 = -((float)oscMessage.Values[0])*2+1; //the message value
		float msgValue1 = -((float)oscMessage.Values[1])*2+1; //the message value
		Debug.Log(msgString); //log the message and values coming from OSC

		switch (msgAddress) {
		case "/wii/irdata":
			if (msgValue0 != -1 && msgValue1 != -1) {
				Debug.Log(msgString);
				tryShoot(msgValue0, msgValue1);
			} else {
				if (readyCount > 0) readyCount--;
			}
			break;

		case "/ipad/irdata":
			Debug.Log(msgString);
			tryShoot(-msgValue1, msgValue0);
			break;
		default:
			break;
		}
	}
}
