using UnityEngine;
using System.Collections;

public class TutorialController : MonoBehaviour {
	private float seconds = 0;
	public float dayNightCycle = 0;
	public float period = 60; // period in seconds
	public float difficultyMultiplier = 1;
	public float berrySpawnInterval = 10; // interval in seconds
	public Sprite[] berrySprites;
	public int maxBerries = 3;
	public int spawnBerries = 1;
	public GameObject berryPrefab;
	GameObject[] bushes;
	public GameObject chickenPrefab;

	private GameObject upgradeChicken;
	private BerryJuiceController berryJuiceController;
	private BasketController basketController;
	private GameObject player;

	// Use this for initialization
	void Start () {
		bushes = GameObject.FindGameObjectsWithTag ("Bush");
		berryJuiceController = GameObject.Find ("berryJuiceController").GetComponent<BerryJuiceController>();
		basketController = GameObject.Find ("Basket").GetComponent<BasketController> ();
		player = GameObject.Find ("Player");
		maxBerries = Mathf.Min (maxBerries, bushes.Length);
		upgradeChicken = Object.Instantiate (chickenPrefab);
		upgradeChicken.SetActive (false);
	}
	GameObject bushWithoutBerry(GameObject[] bushes){
		int rand = Mathf.FloorToInt(Random.value * bushes.Length);
		return bushes[rand];
	}
	// Update is called once per frame
	void Update () {
		if (berryJuiceController.IsFull ()) {
			if(!upgradeChicken.activeInHierarchy){
				upgradeChicken.SetActive (true);
				upgradeChicken.GetComponent<ChickenController> ().SpawnElsewhere ();
			}
			player.GetComponent<PlayerController>().showHeart ();
		} else if (berryJuiceController.IsEmpty ()) {
			player.GetComponent<PlayerController>().showSweat ();
		} else {
			player.GetComponent<PlayerController>().closeBubble();
		}
	}
	void SpawnBerries(){
		if (GameObject.FindGameObjectsWithTag ("Berry").Length < maxBerries) {
			SpawnBerry ();
		}
		Invoke ("SpawnBerries", berrySpawnInterval);
	}
	void SpawnBerry(){
		GameObject bush = bushWithoutBerry(bushes);
		GameObject temp = Object.Instantiate(berryPrefab);
		int berryIndex = Mathf.FloorToInt (berrySprites.Length * Mathf.Min (0.99f, Random.value));
		temp.GetComponent<SpriteRenderer> ().sprite = berrySprites[berryIndex];
		temp.transform.position = bush.transform.position + 
			new Vector3(Random.value * bush.transform.lossyScale.x * 0.5f, Random.value * bush.transform.lossyScale.y * 0.5f, -0.5f);
	}
	public void sendUpdate(int message){

	}
}