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
	private CharacterController characterController;
	public BasketController basketController;

	// Use this for initialization
	void Start () {
		characterController =  gameObject.GetComponent<CharacterController>();
		basketController = GameObject.Find ("Basket").GetComponent<BasketController> ();
	}
	
	void Update () {
		if (satisfied) {
		} else {
			Move ();
		}
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
	}
	void OnControllerColliderHit(ControllerColliderHit hit){
		if(hit.gameObject.CompareTag ("Player") && !satisfied){
			if(basketController.RemoveActiveBerry()){
				satisfied = true;
			}
		}
	}
	void Satisfied() {
		//WALK OFF SCREEN
	}

	void Flip() {
		facingRight *= -1;
		Vector3 currScale = transform.localScale;
		currScale.x *= -1;
		transform.localScale = currScale;
	}
	void OnBecameInvisible(){
		if (satisfied) {
			GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
			int numPlatforms = platforms.Length;
			this.transform.position = platforms[Mathf.FloorToInt(numPlatforms * Mathf.Min (0.99f, Random.value))].transform.position 
				+ new Vector3(0, 5, 0);
			if(this.GetComponent<SpriteRenderer>().isVisible){
				OnBecameInvisible();
			} else{
				satisfied = false;
			}
		}
	}
}
