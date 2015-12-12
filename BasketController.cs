using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasketController : MonoBehaviour {
	public int allowedCapacity = 8;
	private List<Berry> berryList = new List<Berry> ();
	private int activeBerryIndex = -1;
	private GameController gameController;
	public Sprite goodBerry;
	public Sprite badBerry;
	
	// Use this for initialization
	void Start () {
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();
		InvokeRepeating("UpdateEvery30Sec", 0, 30.0F);
	}
	
	// Update is called once per frame
	void Update () {
		if (activeBerryIndex == -1) {
			this.GetComponentInChildren<Text> ().text = "";
		} else {
			Berry activeBerry = berryList[activeBerryIndex];
			this.GetComponentInChildren<Text>().text = ((activeBerry.IsGood())? "+" : "") + activeBerry.GetMultiplier();
			this.GetComponent<Image>().sprite = (activeBerry.IsGood())? goodBerry : badBerry;
		}
		if (Input.GetButtonDown ("Cycle")){
			int berryNum = berryList.Count;
			if(activeBerryIndex != -1 && berryNum != 0){
				activeBerryIndex = (activeBerryIndex + Mathf.RoundToInt(Input.GetAxis ("Cycle")) + berryNum) % berryNum;
			}
		}
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
		if (activeBerryIndex == -1) {
			activeBerryIndex = 0;
		}
		return true;
	}
	
	public int GetTotalMultiplier() {
		int total = 0;
		foreach (Berry berry in berryList) {
			total += berry.GetMultiplier ();
		}
		return total;
	}
	
	Berry GetActiveBerry() {
		if (this.berryList.Count < 1) {
			return null;
		}
		if (this.activeBerryIndex < 0) {
			this.activeBerryIndex = 0;
		}
		return berryList [this.activeBerryIndex];
		
	}

	public bool RemoveActiveBerry() {
		if (berryList.Count < 1) {
			return false;
		} else {
			berryList.RemoveAt(activeBerryIndex);
			activeBerryIndex = Math.Min (activeBerryIndex, berryList.Count - 1);
			return true;
		}
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

	public bool isEmpty() {
		return ! (this.berryList.Count > 0);
	}

	public int GetNextCapacity(){
		return allowedCapacity + 1;
	}
	public void Upgrade(){
		allowedCapacity = GetNextCapacity ();
	}
}
