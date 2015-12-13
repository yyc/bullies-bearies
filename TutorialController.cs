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
	private int state = -1; // private
	private Vector3 chickenPosition;
	GameObject[] bushes;

	// Use this for initialization
	void Start () {
		bushes = GameObject.FindGameObjectsWithTag ("Bush");
		berryJuiceController = GameObject.Find ("berryJuiceController").GetComponent<BerryJuiceController>();
		basketController = GameObject.Find ("Basket").GetComponent<BasketController> ();
		player = GameObject.Find ("Player");
		maxBerries = Mathf.Min (maxBerries, bushes.Length);
		foreach (GameObject berry in tutorialBerries) {
			berry.GetComponent<TutorialBerry>().tutorialController = this.GetComponent<TutorialController>();
			berry.GetComponent<TutorialBerry>().basketController = basketController;

			berry.SetActive (false);
		}
		tutorialBerries [0].SetActive (true);
		upgradeChicken = GameObject.Find ("Chicken");
		upgradeChicken.SetActive (false);
		chickenPosition = upgradeChicken.transform.position;
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
		switch (state) {
		case 1:
			Berry activeBerry = basketController.GetActiveBerry();
			if(!activeBerry.IsGood ()){
				sendUpdate (2);
			}
			break;
		case 2:
			if(basketController.isEmpty() && !tutorialBerries[2].gameObject.activeInHierarchy){
				tutorialBerries[2].gameObject.SetActive (true);
			}
			if(berryJuiceController.IsFull()){
				sendUpdate (3);
			}
			break;
		case 3:
			upgradeChicken.transform.position = chickenPosition;
			if(basketController.allowedCapacity >= 3){
				sendUpdate (4);
			}
			break;
		}
	}

	public void sendUpdate(int message){
		if (message > state) {
			Debug.Log (message);
			state = message;
			switch (message) {
			case 0: // on pickup tutorialBerry0
				//Good! You picked up a berry
				tutorialBerries [1].SetActive (true);
				break;
			case 1: // on pickup tutorialBerry1
				//this berry is BAAD you can see the briefcase has one good and one bad
				//Press Q and E to cycle through the berries

				break;
			case 2: //on cycle to bad berry
				//Now we want to get rid of the bad berry
				invisibleColliders[0].SetActive (false);
				tutorialBerries[2].SetActive (true);
				break;
			case 3: //on full bar
				upgradeChicken.SetActive(true);
				break;
			case 4: //upgraded
				invisibleColliders[1].SetActive(false);
				tutorialBerries[3].SetActive (true);
				break;
			case 5:
				Application.LoadLevel ("StartScene");
				break;
			/*Tutorial:

	- Movement + Berries
	- When you collect the first berry, show the berry meter and juice bar filling slowly, collect the next berry
	- Second berry is negative, will drain juice bar. Introduce briefcase (limit 2) and cycling
	- Introduce Bullies(multiple platforms with multiple bullies), will take berries. Have a third bush that spawns a berry if the player is empty. Goal is to fill the juice bar
	- Introduce chicken to buy larger briefcases
	- Done! Play game.
	*/
			}
		}
	}
	void displayMessage(string msg){

	}
	void hideMessage(){

	}
}