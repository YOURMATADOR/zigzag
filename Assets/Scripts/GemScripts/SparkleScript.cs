using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkleScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (DesactivateAfterTime ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator DesactivateAfterTime (){
		yield return new WaitForSeconds (1.5f);
		gameObject.SetActive (false);
	}
}
