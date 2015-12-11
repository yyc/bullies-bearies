using UnityEngine;
using System.Collections;

public class Respawner : MonoBehaviour
{
	public float respawnTime = 10f;	// The amount of time before spawning starts.
	public GameObject bully;
	public int width;
	public int height;
	private bool dead = false;
	private float waitInHeaven = 0f;
	private float nextRespawn = 0f;

	void Start ()
	{
	}

	
	void Update() {
		if (Time.time >= nextRespawn && dead) { //If the time is due and the enemy is dead
			Respawn(); //create a new enemy
			dead = false; //and tell our script it is alive
		}
	}
	
	private void Respawn ()
	{
		float x = Random.Range (-width / 2, width / 2);
		float y = Random.Range (-height / 2, height / 2);
		Vector3 place = new Vector3 (x, y, 0);
		Instantiate(bully, place, Quaternion.identity);
	}

	void BullySatisfied( Bully bully ) {
		GameObject.Destroy (bully.gameObject);
			dead = true;
			waitInHeaven = Time.time;
			nextRespawn = Time.time + respawnTime;
	}
}
