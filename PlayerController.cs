using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public float Speed = 6f;
	public float Gravity = 20f;
	public float JumpSpeed = 8f;
	public float JumpHeight = 20f;
	public GameObject basket;
	public Sprite heartEmoji;
	public Sprite sweatEmoji;


	private CharacterController characterController;
	
	private Vector3 moveDirection = Vector3.zero;
	
	private bool facingRight = true;
	private GameObject speechBubble;
	private GameObject playerEmoji;

	void Start() {
		characterController = gameObject.GetComponent<CharacterController> ();
		speechBubble = GameObject.Find ("playerSpeechBubble");
		playerEmoji = GameObject.Find ("playerEmoji");
		speechBubble.GetComponent<Animator> ().GetBehaviour<SpeechBubbleBehaviour> ().playerEmoji = playerEmoji;
		closeBubble ();
	}
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
			moveDirection.x *= Speed;
		}
		if (moveDirection.x < 0 && facingRight || moveDirection.x > 0 && !facingRight) {
			Flip ();
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
	void makeBubble(Sprite emoji){
		if (playerEmoji.GetComponent<SpriteRenderer> ().sprite == emoji) {
			return;
		} else {
			speechBubble.GetComponent<SpriteRenderer> ().enabled = true;
			speechBubble.GetComponent<Animator> ().SetTrigger ("Close");
			speechBubble.GetComponent<Animator> ().SetTrigger ("Start");
			playerEmoji.GetComponent<SpriteRenderer> ().sprite = emoji;
		}
	}
	public void closeBubble(){
		playerEmoji.GetComponent<SpriteRenderer> ().enabled = false;
		speechBubble.GetComponent<SpriteRenderer> ().enabled = false;
		playerEmoji.GetComponent<SpriteRenderer> ().sprite = null;
		speechBubble.GetComponent<Animator> ().ResetTrigger ("Start");
		speechBubble.GetComponent<Animator> ().SetTrigger ("Close");
	}
	public void showHeart(){
		makeBubble (heartEmoji);
	}
	public void showSweat(){
		makeBubble (sweatEmoji);
	}
}
