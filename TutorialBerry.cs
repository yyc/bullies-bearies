using UnityEngine;
using System.Collections;

public class TutorialBerry : MonoBehaviour {
	
	public int ControllerMessage;
	public bool isGood;
	public int multiplier;

	public BasketController basketController;
	public TutorialController tutorialController;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Player") && !basketController.IsFull()) {
			tutorialController.sendUpdate(ControllerMessage);
			basketController.AddBerry (new Berry(isGood, multiplier, this.gameObject.GetComponentsInChildren<SpriteRenderer>()[0].sprite));
			this.gameObject.SetActive (false);
		}
	}

}
