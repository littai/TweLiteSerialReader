using UnityEngine;
using System.Collections;

public class SerialTestClient : MonoBehaviour {

	void Awake() {
	}

	void onSerialDataReceived (SerialHandler.TweLiteData data)
	{
//		Debug.Log ("#####DATA RECEIVED:" + data);	
	}

	// Use this for initialization
	void Start () {
		SerialHandler.Instance.Open();
		SerialHandler.Instance.SerialDataReceived += onSerialDataReceived;
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
