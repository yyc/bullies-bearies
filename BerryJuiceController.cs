using UnityEngine;
using System.Collections.Generic;
using System;

public class BerryJuiceController : MonoBehaviour {
	public float maximumJuiceAmount = 5000;
	public float initialJuiceAmount = 100;
	public float lowThreshold = 0;
	public float juiceAmount = 0;
	public int goodBerryCount = 0;
	public int badBerryCount = 0;
	public int multiplierRate = 0;
	public int passiveDrain = 10;
	public GameObject basketObject;
	public GameObject fullMeter;
	public GameObject currentMeter;
	public GameController gameController;

	private BasketController mainBasketController;

	void Start () {
		juiceAmount = initialJuiceAmount;
		mainBasketController = basketObject.GetComponent<BasketController> ();
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
	}
		
	void Update () {
		this.multiplierRate = mainBasketController.GetTotalMultiplier ();
		this.goodBerryCount = mainBasketController.GetGoodBerryCount ();
		this.badBerryCount = mainBasketController.GetBadBerryCount ();
	}
	void FixedUpdate(){ //60fps update
		float tempAmount = this.juiceAmount + (this.multiplierRate - this.passiveDrain * gameController.difficultyMultiplier) * Time.smoothDeltaTime;
		this.juiceAmount = Math.Max (0, Math.Min (tempAmount, maximumJuiceAmount));
		
		float newScaleX = fullMeter.transform.localScale.x;
		float newScaleY = (this.juiceAmount / maximumJuiceAmount) * fullMeter.transform.localScale.y;
		currentMeter.transform.localScale = new Vector3 (newScaleX, newScaleY);
		float newX = fullMeter.transform.position.x;
		float newY = fullMeter.transform.position.y - 
		fullMeter.GetComponent<RectTransform>().rect.height * (1 - (this.juiceAmount / maximumJuiceAmount)) / 2;
		currentMeter.transform.position = new Vector3 (newX, newY);
	}
	public bool IsFull(){
		return juiceAmount == maximumJuiceAmount;
	}
	public bool IsEmpty(){
		return juiceAmount <= lowThreshold;
	}
}