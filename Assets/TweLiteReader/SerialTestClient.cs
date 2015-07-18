using UnityEngine;
using System.Collections;

public class SerialTestClient : MonoBehaviour {

	void Awake() {
	}

	void onPressed(int id) {
		Debug.Log ("onPressed: " + id);
    }
	
	void onReleased(int id) {
		Debug.Log ("onReleased: " + id);
    }

	// Use this for initialization
	void Start () {
		SerialHandler.Instance.Open();
		SerialHandler.Instance.Pressed += onPressed;
		SerialHandler.Instance.Released += onReleased;
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
