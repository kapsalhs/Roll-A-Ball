using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	public float speed;
	public Text countText;
	public Text winText;

	private bool onGround;
	private Rigidbody rb;
	private int count;

	void Start(){
		rb = GetComponent<Rigidbody> ();
		count = 0;
		onGround = true;
		SetCountText ();
		winText.text = "";
	}
	void FixedUpdate(){
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.AddForce (movement * speed);
		if (Input.GetKeyDown (KeyCode.Space) && onGround == true) {
			PlayerJump ();
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag ("Pick Up")) {
			FindObjectOfType<AudioManager>().Play ("Coin Pick Up");
			other.gameObject.SetActive (false);
			count = count + 1;
			SetCountText ();
		}
	}
	void OnCollisionEnter(Collision col){
		if (col.gameObject.CompareTag ("Ground") || col.gameObject.CompareTag ("Wall")) {
			FindObjectOfType<AudioManager>().Play ("Ball Hit");
			onGround = true;
		}
	}
	void SetCountText(){
		countText.text = "Coins: " + count.ToString ();
		if (count >= 12){
			winText.text = "You Win";
		}
	}
	void PlayerJump(){
			Vector3 jump = new Vector3(0.0f,300.0f,0.0f);
			rb.AddForce (jump);
			onGround = false;
	}
}

