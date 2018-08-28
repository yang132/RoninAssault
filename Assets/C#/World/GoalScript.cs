using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour {
	public Transform winMenu;


	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.GetComponentInParent<HumanInput> ()) {
			Instantiate (winMenu, Vector2.zero, Quaternion.identity);
			other.GetComponentInParent<HumanInput> ().defense = 100;
			other.GetComponentInParent<HumanInput> ().nontargetable = true;
			other.GetComponentInParent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezePosition;
		}
	}
}
