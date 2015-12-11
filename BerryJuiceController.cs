using UnityEngine;
using System.Collections.Generic;
using System;

public class BerryJuiceController : MonoBehaviour {
	public const float JUICE_AMOUNT_MAX = 50f;
	public const float JUICE_AMOUNT_INIT = 10f;
	public float juiceAmount = 0;
	public int goodBerryCount = 0;
	public int badBerryCount = 0;
	public int multiplierRate = 0;
//	public GameObject basketObject;
	public GameObject fullMeter;
	public GameObject currentMeter;

//	private BasketController mainBasketController;

	private float initialBarScale;
	private float initialBarY;

	void Start () {
		juiceAmount = JUICE_AMOUNT_INIT;
//		mainBasketController = basketObject.GetComponent<BasketController> ();
		InvokeRepeating("UpdateEvery1Sec", 0, 1);
	}
		
	void Update () {
		if (Input.GetKeyDown ("space")) {
			this.juiceAmount += 10;
		}
//		this.multiplierRate = mainBasketController.GetTotalMultiplier ();
//		this.goodBerryCount = mainBasketController.GetGoodBerryCount ();
//		this.badBerryCount = mainBasketController.GetBadBerryCount ();
	}

	void UpdateEvery1Sec() {
		Debug.Log (this.juiceAmount);
       	this.juiceAmount = Math.Min (this.juiceAmount + this.multiplierRate, JUICE_AMOUNT_MAX);

		float newScaleX = fullMeter.transform.localScale.x;
		float newScaleY = (this.juiceAmount / JUICE_AMOUNT_MAX) * fullMeter.transform.localScale.y;
		currentMeter.transform.localScale = new Vector3 (newScaleX, newScaleY);

		float newX = fullMeter.transform.position.x;
//		float newY = fullMeter.transform.position.y - (newScaleY - fullMeter.transform.localScale.y) / 2;
		float newY = (newScaleY / 2f) - 1f;
		currentMeter.transform.position = new Vector3 (newX, newY);

	}
}