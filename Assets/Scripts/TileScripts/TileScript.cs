using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour {

	[SerializeField] private GameObject gem;
	[SerializeField] private float chanceForCollectable;

	private Rigidbody rigidbody;
	private AudioSource audio;

	void Awake () {
		rigidbody = GetComponent<Rigidbody> ();
		audio = GetComponent<AudioSource> ();
	}

	// Use this for initialization
	void Start () {
		if (Random.value < chanceForCollectable) {
			Vector3 temp = transform.position;
			temp.y += 2f;
			Instantiate (gem, temp, Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerExit (Collider target){
		if (target.tag == "Ball") {
			StartCoroutine (TriggerFallingDown ());
		}
	}

	IEnumerator TriggerFallingDown () {
		yield return new WaitForSeconds (0.3f);
		rigidbody.isKinematic = false;
		audio.Play ();
		StartCoroutine (TurnOffGameObject());
	}

	IEnumerator TurnOffGameObject () {
		yield return new WaitForSeconds (2f);
		gameObject.SetActive (false); // opcion 2: destroy (gameObject);
	}
}
