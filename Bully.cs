using UnityEngine;
using System.Collections;

public class Bully : MonoBehaviour {

	public bool satisfied;
	public Respawner respawner;
	public float Speed = 6f;
	public float Gravity = 20f;
	public float JumpSpeed = 8f;
	public float JumpHeight = 20f;	
	private int facingRight = 1;
	private Vector3 moveDirection = Vector3.zero;	
	private CharacterController characterController;
	public BasketController basketController;

	// Use this for initialization
	void Start () {
		characterController =  gameObject.GetComponent<CharacterController>();
	}
	
	void Update () {
		if (satisfied) {
			Satisfied();
		}
		Move ();
	}

	void OnMouseDown() {

	}

	void Move() {
		if (characterController.isGrounded) {
			moveDirection = new Vector3 (facingRight, 0, 0);
			moveDirection = transform.TransformDirection (moveDirection);
			moveDirection *= Speed;
		} else {
			moveDirection = new Vector3(facingRight, moveDirection.y, 0);
			moveDirection = transform.TransformDirection (moveDirection);
			moveDirection.x *= Speed / 2;
		}
		moveDirection.y -= Gravity * Time.deltaTime;
		characterController.Move (moveDirection * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Platform Edge")) {
			Flip ();
		}
		if (other.CompareTag ("Player")) {
			if (!basketController.isEmpty()) {
			}
		}
	}
	void Satisfied() {
		//WALK OFF SCREEN
		respawner.SendMessage ("BullySatisfied", this);
	}

	void Flip() {
		facingRight *= -1;
		Vector3 currScale = transform.localScale;
		currScale.x *= -1;
		transform.localScale = currScale;
	}
}
