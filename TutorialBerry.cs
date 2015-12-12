using UnityEngine;
using System.Collections;

public class TutorialBerry : MonoBehaviour {
	
	public int ControllerMessage;
	public bool isGood;
	public int multiplier;

	private BasketController basket;
	private TutorialController tutorialController;
	// Use this for initialization
	void Start () {
		basket = GameObject.Find ("Basket").GetComponent<BasketController> ();
		tutorialController = GameObject.Find ("TutorialController").GetComponent<TutorialController> ();
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnTriggerEnter(Collider other) {
		if (basket == null) {
			basket = GameObject.Find ("Basket").GetComponent<BasketController> ();
		}
		if (other.gameObject.CompareTag ("Player") && !basket.IsFull()) {
			tutorialController.sendUpdate(ControllerMessage);
			basket.AddBerry (new Berry(isGood, multiplier, this.gameObject.GetComponentsInChildren<SpriteRenderer>()[0].sprite));
			Destroy (gameObject);
		}
	}

}
