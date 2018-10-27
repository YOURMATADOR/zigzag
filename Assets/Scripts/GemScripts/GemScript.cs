using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemScript : MonoBehaviour {

	private float speed = 1f;
	private float angle;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		angle = (angle + speed) % 360f;
		transform.localRotation = Quaternion.Euler (45f, angle, 45f);
	}
}
