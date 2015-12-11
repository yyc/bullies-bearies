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
	// Use this for initialization
	void Start () {
		juiceAmount = startingJuiceAmount;
		mainBasketController = basketObject.GetComponent<BasketController> ();
	}

	// Update is called once per frame
	void Update () {
		this.multiplierRate = mainBasketController.GetTotalMultiplier ();
		this.juiceAmount += this.multiplierRate;
		this.goodBerryCount = mainBasketController.GetGoodBerryCount ();
		this.badBerryCount = mainBasketController.GetBadBerryCount ();
		print (multiplierRate);
		print (juiceAmount);
		print (goodBerryCount);
		print (badBerryCount);
	}
}