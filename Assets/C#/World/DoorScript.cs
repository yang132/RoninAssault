using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {
	public Transform key;


	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
		if (key == null) {
			Destroy (gameObject);
		}
	}
}
