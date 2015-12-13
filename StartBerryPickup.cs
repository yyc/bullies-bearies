using UnityEngine;
using System.Collections;

public class StartBerryPickup : MonoBehaviour {
	public string SceneToLoad;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log ("entering scene " + SceneToLoad);
		Application.LoadLevel (SceneToLoad);
	}
	void OnTriggerEnter(){
		Debug.Log ("hi");
	}
}