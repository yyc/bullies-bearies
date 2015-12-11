using UnityEngine;
using System.Collections.Generic;
using System;

public class BerryJuiceController : MonoBehaviour {
	public const int JUICE_AMOUNT_MAX = 50;
	public const int JUICE_AMOUNT_INIT = 10;
	public int juiceAmount = 0;
	public int goodBerryCount = 0;
	public int badBerryCount = 0;
	public int multiplierRate = 0;
	public GameObject basketObject;
	public GameObject fullMeter;
	public GameObject currentMeter;

	private BasketController mainBasketController;

	void Start () {
		juiceAmount = JUICE_AMOUNT_INIT;
		mainBasketController = basketObject.GetComponent<BasketController> ();
		InvokeRepeating("UpdateEvery1Sec", 0, 10.0F);
	}
		
	void Update () {
		if (Input.GetKeyDown ("space")) {
			this.juiceAmount += 10;
		}

		this.multiplierRate = mainBasketController.GetTotalMultiplier ();
		this.goodBerryCount = mainBasketController.GetGoodBerryCount ();
		this.badBerryCount = mainBasketController.GetBadBerryCount ();
	}

	void UpdateEvery1Sec() {
		int tempAmount = this.juiceAmount + this.multiplierRate;
		this.juiceAmount = Math.Min (tempAmount, JUICE_AMOUNT_MAX);

		float newScaleX = fullMeter.transform.localScale.x;
		float newScaleY = (this.juiceAmount / JUICE_AMOUNT_MAX) * fullMeter.transform.localScale.y;
		currentMeter.transform.localScale = new Vector3 (newScaleX, fullMeter.transform.localScale.y + 1);

		float newX = fullMeter.transform.position.x;
		float newY = fullMeter.transform.position.y - (newScaleY - fullMeter.transform.localScale.y) / 2;
		currentMeter.transform.position = new Vector3 (newX, newY);
	}
}