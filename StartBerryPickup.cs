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
		print ("entering scene " + SceneToLoad);
		Application.LoadLevel (SceneToLoad);
	}
}