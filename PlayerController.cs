using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public float Speed = 6f;
	public float Gravity = 20f;
	public float JumpSpeed = 8f;
	public float JumpHeight = 20f;
	
	public CharacterController characterController;
	
	private Vector3 moveDirection = Vector3.zero;
	
	private bool facingRight = true;
	public GameObject basket;
	// Every frame
	void Update () {
		
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
			moveDirection.x *= Speed / 2;
		}
		if (moveDirection.x < 0 && facingRight || moveDirection.x > 0 && !facingRight) {
			Flip ();
		}
		if (Input.GetKeyDown ("space")) {
			basket.GetComponent<BasketController>().AddBerry(new Berry(true, 20));
		}
		moveDirection.y -= Gravity * Time.deltaTime;
		characterController.Move (moveDirection * Time.deltaTime);
	}
	
	void Flip() {
		facingRight = !facingRight;
		Vector3 currScale = transform.localScale;
		currScale.x *= -1;
		transform.localScale = currScale;
	}
}
