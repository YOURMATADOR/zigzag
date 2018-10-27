using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	private Transform ballTarget;
	private Vector3 oldPosition;

	void Awake () {
		ballTarget = GameObject.FindGameObjectWithTag ("Ball").transform;
		oldPosition = ballTarget.position;
	}

	// Use this for initialization 
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate () {
		if (ballTarget != null) {
			Vector3 newPosition = ballTarget.position;
			Vector3 delta = oldPosition - newPosition;
			delta.y = 0f;
			transform.position = transform.position - delta;
			oldPosition = newPosition;
		}
	}
}
