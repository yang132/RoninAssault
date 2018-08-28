using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAction : EnemyAction {

	// Use this for initialization
	
	// Update is called once per frame
	public override void move(Vector2 myDirection, Collider2D h)
	{


		Vector2 vdiff = -myRigid.position + (Vector2)h.transform.position;
		myRigid.MoveRotation (myRigid.rotation + turnSpeed * Vector2.SignedAngle (myDirection, vdiff));
	}
}
