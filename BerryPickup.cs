﻿using UnityEngine;
using System.Collections;

public class BerryPickup : MonoBehaviour {
	
	public BasketController basket;
	
	// Use this for initialization
	void Start () {
		basket = GameObject.Find ("Basket").GetComponent<BasketController> ();
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnTriggerEnter(Collider other) {
		if (basket == null) {
			basket = GameObject.Find ("Basket").GetComponent<BasketController> ();
		}
		if (other.gameObject.CompareTag ("Player") && !basket.IsFull()) {
//			gameObject.SetActive (false);

			basket.AddBerry (new Berry(this.gameObject.GetComponentsInChildren<SpriteRenderer>()[0].sprite));
			Destroy (gameObject);
		}
	}
}