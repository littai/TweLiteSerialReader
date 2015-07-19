using UnityEngine;
using System.Collections;

public class SerialTestClient : MonoBehaviour {

	void Awake() {
	}

	void onPressed(int id) {
		Debug.Log (System.DateTime.Now + " onPressed: " + id);
    }
	
	void onReleased(int id) {
		Debug.Log (System.DateTime.Now + " onReleased: " + id);
    }

	void onSwitchOn(int id) {
		Debug.Log (System.DateTime.Now + " onSwitchOn: " + id);
	}

	void onSwitchOff(int id) {
		Debug.Log(System.DateTime.Now + " onSwitchOff: " + id);
	}

	// Use this for initialization
	void Start () {
		SerialHandler.Instance.Open();
		SerialHandler.Instance.Pressed += onPressed;
		SerialHandler.Instance.Released += onReleased;
		SerialHandler.Instance.SwitchOn += onSwitchOn;
		SerialHandler.Instance.SwitchOff += onSwitchOff;
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
