using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
	public float speed;
	public Text countText;
	public float jumpForce;

	private bool onGround;
	private Rigidbody rb;
	private int count;

	void Start(){
		rb = GetComponent<Rigidbody> ();
		count = 0;
		onGround = true;
		SetCountText ();
	}
	void FixedUpdate(){
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.AddForce (movement * speed);
		if (Input.GetKeyDown (KeyCode.Space) && onGround == true) {
			PlayerJump ();
		}
		if (gameObject.transform.position.y < -10) {
			SceneManager.LoadScene ("MiniGame");
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
	void OnCollisionExit(Collision col){
		if (col.gameObject.CompareTag ("Ground") || col.gameObject.CompareTag ("Wall")) {
			onGround = false;
		}
	}

	void SetCountText(){
		countText.text = "Coins: " + count.ToString ();
		if (count >= 35){
			SceneManager.LoadScene ("Win Scene");
		}
	}
	void PlayerJump(){
		Vector3 jump = new Vector3(0.0f,jumpForce,0.0f);
		rb.AddForce (jump * jumpForce);
		onGround = false;
	}
}

