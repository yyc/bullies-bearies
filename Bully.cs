using UnityEngine;
using System.Collections;

public class Bully : MonoBehaviour {

	public bool satisfied;
	public float Speed = 6f;
	public float Gravity = 20f;
	public float JumpSpeed = 8f;
	public float JumpHeight = 20f;	
	private int facingRight = 1;
	private Vector3 moveDirection = Vector3.zero;	
	public int width;
	public int height;



	// Use this for initialization
	void Start () {
	
	}
	
	void Update () {
		if (satisfied) {
			Satisfied();
		}
		Move ();
	}

	void OnMouseDown() {
		print ("bully clicked");
		Satisfied ();
	}

	void Move() {
		moveDirection = new Vector3 (facingRight, 0, 0);
		moveDirection = transform.TransformDirection (moveDirection);
		gameObject.GetComponent<CharacterController>().Move (moveDirection * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("PlatformEdge")) {
			Flip ();
		}
	}

	void Satisfied() {
		makeSpriteFriendly ();
	}

	void makeSpriteFriendly() {
	}

	void makeSpriteHostile() {
	}

	void Flip() {
		facingRight *= -1;
		Vector3 currScale = transform.localScale;
		currScale.x *= -1;
		transform.localScale = currScale;
	}

	void OnBecameInvisible() {
		if (this.satisfied) {
			float x = Random.Range (-width / 2, width / 2);
			float y = Random.Range (-height / 2, height / 2);
			gameObject.transform.position = new Vector3 (x, y, 0);
			makeSpriteHostile();
		}
	}
}
