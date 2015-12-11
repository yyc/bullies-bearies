using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	
	//public GameObject player;
	public GameObject player;
	private Vector3 offset;
	
	// Use this for initialization
	void Start () {
		//player = GameObject.FindGameObjectWithTag ("Player");
		offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		//player = GameObject.FindGameObjectWithTag ("Player");
		transform.position = player.transform.position + offset;
	}
}
