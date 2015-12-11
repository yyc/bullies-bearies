using System;
using System.Collections.Generic;
using UnityEngine;

public class BasketController : MonoBehaviour {
	public int allowedCapacity = 8;
	public int numBerries = 0;
	public bool activeBerryStatus = true;
	public bool activeBerryMultiplier = 0;
	private List<Berry> berryList = new List<Berry> ();
	private int activeBerryIndex = -1;
	private GameController gameController;

	// Use this for initialization
	void Start () {
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();
		InvokeRepeating("UpdateEvery30Sec", 0, 30.0F);
	}
	
	// Update is called once per frame
	void Update () {
		this.numBerries = this.berryList.Count;
		Berry activeBerry = berryList [this.activeBerryIndex];
		this.activeBerryMultiplier = activeBerry.GetMultiplier ();
		this.activeBerryStatus = activeBerry.IsGood ();
	}

	void UpdateEvery30Sec() {
		bool isDay = gameController.dayNightCycle > 0;
		float largeProbability = Math.Abs (gameController.dayNightCycle) * 0.1F;
		float smallProbability = Math.Abs (gameController.dayNightCycle) * 0.01F;
		float goodToBoodProbability = (isDay) ? smallProbability : largeProbability;
		float badToGoodProbability = (isDay) ? largeProbability : smallProbability;
		System.Random rng = new System.Random();

		foreach (Berry berry in berryList) {
			float randFloat = (float) rng.NextDouble ();
			if (berry.IsGood ()) {
				if (randFloat <= goodToBoodProbability) {
					berry.ToggleStatus ();
				}
			} else {
				if (randFloat <= badToGoodProbability) {
					berry.ToggleStatus ();
				}
			}
		}
	}

	public bool IsFull() {
		return berryList.Count == allowedCapacity;
	}

	public void SetBerryCapacity(int capacity) {
		this.allowedCapacity = capacity;
	}

	public bool AddBerry(Berry newBerry) {
		if (berryList.Count + 1 > allowedCapacity) {
			return false;
		}
		berryList.Add (newBerry);
		return true;
	}

	public int GetTotalMultiplier() {
		int total = 0;
		foreach (Berry berry in berryList) {
			total += berry.GetMultiplier ();
		}
		return total;
	}

	public Berry GetActiveBerry() {
		if (this.berryList.Count < 1) {
			return null;
		}
		if (this.activeBerryIndex < 0) {
			this.activeBerryIndex = 0;
		}
		return berryList [this.activeBerryIndex];

	}

	public bool RemoveActiveBerry() {
		if (this.berryList.Count < 1) {
			return false;
		}
		berryList.RemoveAt (activeBerryIndex);
		if (berryList.Count == activeBerryIndex) {
			activeBerryIndex--;
		}
		return true;
	}

	public void SwitchActiveBerry() {
		if (berryList.Count < 1) {
			return;
		}

		this.activeBerryIndex = (this.activeBerryIndex + 1) % this.berryList.Count;
	}

	public int GetGoodBerryCount() {
		int goodBerryCount = 0;
		foreach (Berry berry in berryList) {
			if (berry.IsGood ()) {
				goodBerryCount += 1;
			}
		}
		return goodBerryCount;
	}

	public int GetBadBerryCount() {
		int badBerryCount = 0;
		foreach (Berry berry in berryList) {
			if (!berry.IsGood ()) {
				badBerryCount += 1;
			}
		}
		return badBerryCount;
	}
}
