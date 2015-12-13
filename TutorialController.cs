using UnityEngine;
using System.Collections;

public class TutorialController : MonoBehaviour {
	private float seconds = 0;
	public float dayNightCycle = 0;
	public float period = 60; // period in seconds
	public float difficultyMultiplier = 1;
	public float berrySpawnInterval = 10; // interval in seconds
	public int maxBerries = 3;
	public int spawnBerries = 1;
	public GameObject berryPrefab;
	public GameObject chickenPrefab;
	public GameObject[] tutorialBerries;
	public GameObject[] invisibleColliders;

	private GameObject upgradeChicken;
	private BerryJuiceController berryJuiceController;
	private BasketController basketController;
	private GameObject player;
	public int state = 0; // private
	GameObject[] bushes;

	// Use this for initialization
	void Start () {
		bushes = GameObject.FindGameObjectsWithTag ("Bush");
		berryJuiceController = GameObject.Find ("berryJuiceController").GetComponent<BerryJuiceController>();
		basketController = GameObject.Find ("Basket").GetComponent<BasketController> ();
		player = GameObject.Find ("Player");
		maxBerries = Mathf.Min (maxBerries, bushes.Length);
		foreach (GameObject berry in tutorialBerries) {
			berry.SetActive (false);
		}
		tutorialBerries [0].SetActive (true);
//		upgradeChicken = Object.Find ("TutorialChicken");
//		upgradeChicken.SetActive (false);
	}
	GameObject bushWithoutBerry(GameObject[] bushes){
		int rand = Mathf.FloorToInt(Random.value * bushes.Length);
		return bushes[rand];
	}
	// Update is called once per frame
	void Update () {
		if (berryJuiceController.IsFull ()) {
			player.GetComponent<PlayerController>().showHeart ();
		} else if (berryJuiceController.IsEmpty ()) {
			player.GetComponent<PlayerController>().showSweat ();
		} else {
			player.GetComponent<PlayerController>().closeBubble();
		}
	}

	public void sendUpdate(int message){

		switch (message){
		case 1:
			tutorialBerries[1].SetActive (true);
			Debug.Log (tutorialBerries[1].activeInHierarchy);
		break;
		}
	}
	void displayMessage(string msg){

	}
	void hideMessage(){

	}
}