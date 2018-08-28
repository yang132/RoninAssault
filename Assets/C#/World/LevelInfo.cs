using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo : MonoBehaviour {
	public string nextLevel;

	void Start()
	{
		GameObject player = GameObject.Find ("Ronin");
		if (player == null) {
			Debug.Log ("Error: Ronin not found");
		} else {
			player.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezePosition;

			//player.GetComponent<HumanInput> ().enabled = false;
		}

	}

}
