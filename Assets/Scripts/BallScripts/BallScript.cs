using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {

	private Rigidbody rigidbody;
	private bool rollLeft;

	public float speed = 4f;

	void Awake () {
		rigidbody = GetComponent<Rigidbody> ();
		rollLeft = true;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		CheckInput ();
		CheckBallOffBounds ();
	}

	void FixedUpdate (){
		if (GameController.instance.gamePlaying == true) {
			if (rollLeft == true) {
				rigidbody.velocity = new Vector3 (-speed, Physics.gravity.y, 0f);
			}else{
				rigidbody.velocity = new Vector3 (0f, Physics.gravity.y, speed);
			}
		}
	}

	void CheckInput () {
		if (Input.GetMouseButtonDown (0)) {
			if (!GameController.instance.gamePlaying) {
				GameController.instance.gamePlaying = true;
			}
		}
		if (Input.GetMouseButtonDown (1)) {
			if (GameController.instance.gamePlaying) {
				rollLeft = !rollLeft;
				GameController.instance.ActiveTileSpawner ();
			}
		}
	}

	void CheckBallOffBounds (){
		if (GameController.instance.gamePlaying) {
			if (transform.position.y < -3) {
				GameController.instance.gamePlaying = false;
				Destroy (gameObject);
			}
		}
	}
}