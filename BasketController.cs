using UnityEngine;
using System.Collections.Generic;

public class BasketController : MonoBehaviour {
	public int allowedCapacity = 8;
	private List<Berry> berryList = new List<Berry> ();
	private int activeBerryIndex = -1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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

	Berry GetActiveBerry() {
		if (this.berryList.Count < 1) {
			return null;
		}
		if (this.activeBerryIndex < 0) {
			this.activeBerryIndex = 0;
		}
		return berryList [this.activeBerryIndex];

	}

	void SwitchActiveBerry() {
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
