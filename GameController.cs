using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public float dayNightCycle = 0;
	public float period = 60; // period in seconds
	public float berrySpawnInterval = 10000; // interval in seconds
	public int maxBerries = 3;
	public int spawnBerries = 1;
	public GameObject berryPrefab;

	private float seconds = 0;


	// Use this for initialization
	void Start () {
		GameObject[] bushes = GameObject.FindGameObjectsWithTag ("Bush");
		maxBerries = Mathf.Min (maxBerries, bushes.Length);
		GameObject temp;
		GameObject bush;
		for (int i = 0; i < spawnBerries; i++) {
			bush = bushWithoutBerry(bushes);
			temp = Object.Instantiate(berryPrefab);
			temp.transform.parent = bush.transform;
			temp.transform.localPosition = new Vector3(0, 0, -2);
		}
		InvokeRepeating ("EverySecond", 0, 1);
		Invoke ("SpawnBerries", berrySpawnInterval);
	}
	GameObject bushWithoutBerry(GameObject[] bushes){
		int rand = Mathf.FloorToInt(Random.value * bushes.Length);
		if (bushes [rand].transform.childCount >= 1) {
			return bushWithoutBerry (bushes);
		} else {
			return bushes[rand];
		}

	}
	// Update is called once per frame
	void Update () {
	}
	void EverySecond(){
		seconds += 1f;
		dayNightCycle = Mathf.Sin (seconds * 2 * Mathf.PI / period);
	}
	void SpawnBerries(){
		if (GameObject.FindGameObjectsWithTag ("Berry").Length < maxBerries) {
			GameObject temp;
			GameObject bush;
			bush = bushWithoutBerry (GameObject.FindGameObjectsWithTag ("Bush"));
			temp = Object.Instantiate (berryPrefab);
			temp.transform.parent = bush.transform;
			temp.transform.localPosition = new Vector3 (0, 0, -2);
		}
		Invoke ("SpawnBerries", berrySpawnInterval);
	}
}
