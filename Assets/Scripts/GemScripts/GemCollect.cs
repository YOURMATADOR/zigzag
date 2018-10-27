using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemCollect : MonoBehaviour {

	[SerializeField] 
	private GameObject sparkleFX;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider other){
		if (other.tag == "Ball") {
			Instantiate (sparkleFX, transform.position, Quaternion.identity);
			GameController.instance.PlayCollectableSound ();
			gameObject.SetActive (false);

		}
	}
}
