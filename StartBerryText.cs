using UnityEngine;
using System.Collections;

public class StartBerryText : MonoBehaviour {
	public string msg = "";
	private string defaultMessage;
	public TextMesh berryText;

	// Use this for initialization
	void Start () {
		defaultMessage = berryText.text;
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter() {
		berryText.text = msg;
	}

	void OnTriggerExit() {
		berryText.text = defaultMessage;
	}
}
