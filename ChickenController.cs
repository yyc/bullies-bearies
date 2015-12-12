using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChickenController : MonoBehaviour {
	private BasketController basketController;
	GameObject upgradeDialog;
	// Use this for initialization
	void Start () {
		basketController = GameObject.Find ("Basket").GetComponent<BasketController> ();
		upgradeDialog = GameObject.Find ("UpgradeDialog");
	}
	
	// Update is called once per frame
	void Update () {
		if (upgradeDialog.GetComponent<Canvas> ().enabled && Input.GetButtonDown ("Cycle")) {
			if(Input.GetAxis ("Cycle") < 0){
				basketController.Upgrade ();
			} else{
				SpawnElsewhere ();
			}
			upgradeDialog.GetComponent<Canvas> ().enabled = false;
		}
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Player")) {
			upgradeDialog.GetComponent<Canvas>().enabled = true;
//			upgradeDialog.GetComponent<Canvas>().planeDistance = 100;
			foreach (Text text in upgradeDialog.GetComponentsInChildren<Text>()){
				text.enabled = false;
				text.enabled = true;
			}
			upgradeDialog.transform.FindChild ("UpgradeText").gameObject.GetComponent<Text> ().text = "Upgrade Briefcase Capacity to "
				+ basketController.GetNextCapacity();
		} else if (other.gameObject.CompareTag ("Bully")) {
			SpawnElsewhere();
		}
	}
	void UpgradeBriefcase(){

	}
	void SpawnElsewhere(){
		GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
		int numPlatforms = platforms.Length;
		this.transform.position = platforms[Mathf.FloorToInt(numPlatforms * Mathf.Min (0.99f, Random.value))].transform.position 
			+ new Vector3(0, 5, 0);
		if(this.gameObject.GetComponent<SpriteRenderer>().isVisible){
			SpawnElsewhere();
		} else{
		}
	}
}
