using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChickenController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Player")) {
			GameObject upgradeDialog = GameObject.Find ("UpgradeDialog");
			upgradeDialog.SetActive (true);
			upgradeDialog.transform.FindChild ("UpgradeText").gameObject.GetComponent<Text>().text = "hi";
			Destroy (gameObject);
		}
	}
	void UpgradeBriefcase(){

	}
}
