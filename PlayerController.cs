using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public float Speed = 6f;
	public float Gravity = 20f;
	public float JumpSpeed = 10f;
	public float JumpHeight = 15f;
	
	private CharacterController characterController;
	
	private Vector3 moveDirection = Vector3.zero;
	
	private bool facingRight = true;

	void Start(){
		characterController = GameObject.FindGameObjectWithTag ("Player").GetComponent<CharacterController> ();
	}

	// Every frame
	void FixedUpdate () {
		
		if (characterController.isGrounded) {
			moveDirection = new Vector3 (Input.GetAxis ("Horizontal"), 0, 0);
			moveDirection = transform.TransformDirection (moveDirection);
			moveDirection *= Speed;
			if (Input.GetKeyDown ("up")) {
				moveDirection.y = JumpSpeed * JumpHeight / 10; 
			}
		} else {
			moveDirection = new Vector3(Input.GetAxis ("Horizontal"), moveDirection.y, 0);
			moveDirection = transform.TransformDirection (moveDirection);
//			moveDirection.x *= Speed / 2; // original airspeed was halved
			moveDirection.x *= Speed;
		}
		if (moveDirection.x < 0 && facingRight || moveDirection.x > 0 && !facingRight) {
			Flip ();
		}
		moveDirection.y -= Gravity / 60;
		characterController.Move (moveDirection / 60);
	}
	
	void Flip() {
		facingRight = !facingRight;
		Vector3 currScale = transform.localScale;
		currScale.x *= -1;
		transform.localScale = currScale;
	}
	void onTriggerEnter( Collider col){
		if(col.gameObject.CompareTag ("Berry")){
			Debug.Log ("hit berry");
		}
	}
}
