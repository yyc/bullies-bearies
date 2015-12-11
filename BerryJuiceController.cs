using UnityEngine;
using System.Collections.Generic;

public class BerryJuiceController : MonoBehaviour {
	public int startingJuiceAmount = 0;
	public int juiceAmount = 0;
	public int goodBerryCount = 0;
	public int badBerryCount = 0;
	public int multiplierRate = 0;
	public GameObject basketObject;

	private BasketController mainBasketController;

	void Start () {
		juiceAmount = startingJuiceAmount;
		mainBasketController = basketObject.GetComponent<BasketController> ();
		InvokeRepeating("UpdateEvery1Sec", 0, 2.0F);
	}
		
	void Update () {
		this.multiplierRate = mainBasketController.GetTotalMultiplier ();
		this.goodBerryCount = mainBasketController.GetGoodBerryCount ();
		this.badBerryCount = mainBasketController.GetBadBerryCount ();
	}

	void UpdateEvery1Sec() {
		this.juiceAmount += this.multiplierRate;
	}
}