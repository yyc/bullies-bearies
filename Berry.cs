using UnityEngine;
using System;

public class Berry{
	private bool status;
	private int multiplier;
	public float minMultiplier = 10;
	public float maxMultiplier = 50;
	public Sprite berryImage;

	public Berry (bool status, int multiplier) {
		this.status = status;
		multiplier = Math.Abs (multiplier);
		this.multiplier = (status) ? (multiplier) : -1 * multiplier;
	}
	public Berry(Sprite img){
		berryImage = img;
		GameController gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		if (((UnityEngine.Random.value * 2) - 1 + gameController.dayNightCycle) >= 0) {
			this.status = true;
		} else {
			this.status = false;
		}
		this.multiplier = Mathf.RoundToInt(((status) ? 1f : -1f) * (UnityEngine.Random.value * (maxMultiplier - minMultiplier) + minMultiplier));
	}

	public void ToggleStatus() {
		this.status = !this.status;
		this.multiplier *= -1;
	}

	public bool IsGood() {
		return this.status;
	}

	public int GetMultiplier() {
		return this.multiplier;
	}
}