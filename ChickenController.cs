using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChickenController : MonoBehaviour {
	private BasketController basketController;
	private Animator animator;
	GameObject upgradeDialog;
	// Use this for initialization
	void Start () {
		Debug.Log ("Chicken Spawned");
		basketController = GameObject.Find ("Basket").GetComponent<BasketController> ();
		upgradeDialog = GameObject.Find ("UpgradeDialog");
		animator = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (upgradeDialog.GetComponent<Canvas> ().enabled && Input.GetButtonDown ("Cycle")) {
			if(Input.GetAxis ("Cycle") < 0){
				basketController.Upgrade ();
			} else{
				animator.SetTrigger("collideWithBully");
//				SpawnElsewhere ();
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
			animator.SetTrigger("collideWithBully");
//			SpawnElsewhere();
		}
	}
	void UpgradeBriefcase(){

	}
	public void SpawnElsewhere(){
		GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
		int numPlatforms = platforms.Length;
		int newIndex = Mathf.FloorToInt (numPlatforms * Mathf.Min (0.99f, Random.value));
		this.transform.position = platforms[newIndex].transform.position 
			+ new Vector3(0, (platforms[newIndex].transform.localScale.y + this.GetComponent<BoxCollider>().size.y * this.transform.localScale.y) / 2, 0);
		//			SpawnElsewhere(platforms, newIndex);
	}
}
