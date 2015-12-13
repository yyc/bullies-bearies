using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
	private Text bearWindowText;
	private int state = -1; // private
	private Vector3 chickenPosition;
	private string[,] tutorialDialogue = new string[10,10];
	private int dialogueIndex = 0;
	GameObject[] bushes;

	// Use this for initialization
	void Start () {
		bushes = GameObject.FindGameObjectsWithTag ("Bush");
		berryJuiceController = GameObject.Find ("berryJuiceController").GetComponent<BerryJuiceController>();
		basketController = GameObject.Find ("Basket").GetComponent<BasketController> ();
		player = GameObject.Find ("Player");
		bearWindowText = GameObject.Find ("BearWindow").GetComponentInChildren<Text> ();
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
		tutorialDialogue[0,0] = "First day on the job, newbie? Press Space to continue.";
		tutorialDialogue[0, 1] = "Guess I'll have to show you the ropes. ";
		tutorialDialogue[0,2] = "Use the arrow keys to move around and jump!";
		tutorialDialogue[0,3] = "Didn't your mother teach you how to do this?";
		tutorialDialogue[0,4] = "Our office is powered solely by Bearry Juice from Bearries.";
		tutorialDialogue[0,5] = "As you can see, we're running low. That's why it's so dark in here!";
		tutorialDialogue[0,6] = "To pick up a Bearry, walk over it. Try finding one now.";
		
		tutorialDialogue[1,0] = "Look at that! Your Bearry Juice Bar is increasing!";
		tutorialDialogue[1,1] = "You only have space in your briefcase for a few Bearies at a time.";
		tutorialDialogue [1, 2] = "Pick up another! We need more!";

		tutorialDialogue[2,0] = "Oh no, Your Juice Bar is draining!";
		tutorialDialogue[2,1] = "Try pressing Q and E to change your active Bearry."; 

		tutorialDialogue[3,0] = "Oh... That's a bad one. It's draining your Juice Bar instead!"; 
		tutorialDialogue[3,1] = "Well, I'll show you how to get rid of it."; 
		tutorialDialogue[3,2] = "Find a Bully. They're usually pretty pissed off, but they love Bearries."; 
		tutorialDialogue[3,3] = "His tastebud deficiencies mean he can't tell whether Bearries are bad or good!"; 
		tutorialDialogue[3,4] = "Bump into the Bully with the bad Bearry active!"; 
		tutorialDialogue[3,5] = "Try to fill up the Juice Bar! I'm just gonna wait here."; 

		tutorialDialogue[4,0] = "Look at that! Pesky Business Chickens can smell when you have plentiful Bearry Juice."; 
		tutorialDialogue[4,1] = "Buy something from him, and he'll go away. Looks like this guy sells briefcases."; 
		tutorialDialogue[4,2] = "Be careful, because Business Chickens take half your current Juice as payment!"; 

		tutorialDialogue[5,0] = "Don't let your Juice Bar reach 0! Or else I'll yell at you!";
		tutorialDialogue[5,1] = "That's all you need to know. Now go get us some Bearries, newbie!"; 		displayMessage (0);
	}
	GameObject bushWithoutBerry(GameObject[] bushes){
		int rand = Mathf.FloorToInt(UnityEngine.Random.value * bushes.Length);
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
		if (Input.GetKeyDown ("space")) {
			displayMessage (dialogueIndex + 1);
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
			displayMessage(0);
		}
	}
	void displayMessage(int index){
		if(tutorialDialogue[state + 1, index] != null){
			bearWindowText.text = tutorialDialogue[state + 1, index];
			dialogueIndex = index;
		}
	}
	void hideMessage(){

	}
}