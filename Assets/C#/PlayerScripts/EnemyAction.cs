using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : Hittable {
	public float fVision;
	public float bVision;
	public float sVision;
	public float attackRate;
	public float attackRange;
	public float turnSpeed = .25f;
	public GameObject spawnedAttack;
	protected Rigidbody2D myRigid;
	private float cooldown;

	// Use this for initialization
	void Start () {
		myRigid = GetComponent<Rigidbody2D> ();
		setHealth();
		setID (2);
		cooldown = 0;
	}

	public virtual void move(Vector2 myDirection, Collider2D h)
	{
		myRigid.AddForce (1500 * Time.deltaTime * (new Vector2 (-Mathf.Sin (myRigid.rotation / 180 * Mathf.PI), 
			Mathf.Cos (myRigid.rotation / 180 * Mathf.PI))));


		Vector2 vdiff = -myRigid.position + (Vector2)h.transform.position;
		myRigid.MoveRotation (myRigid.rotation + turnSpeed * Vector2.SignedAngle (myDirection, vdiff));
	}

	public virtual void attack (Vector2 myDirection, Collider2D h)
	{
		if (cooldown <= 0 && Vector2.Distance (myRigid.position, (Vector2)h.transform.position) < attackRange) { //attacking range and cooldown check
			if (Vector2.Angle (myDirection, (Vector2)h.transform.position - myRigid.position) < 15) {

				cooldown = attackRate;
				//Debug.Log ("hits");
				float xdiff = myRigid.position.x - Mathf.Sin (myRigid.rotation / 180 * Mathf.PI) * .7755346f;
				float ydiff = myRigid.position.y + Mathf.Cos (myRigid.rotation / 180 * Mathf.PI) * .7755346f;

				//.7755436
				GameObject instantiated;
				instantiated = Instantiate (spawnedAttack, new Vector2 (xdiff, ydiff), Quaternion.AngleAxis (myRigid.rotation, Vector3.forward)).gameObject;
				instantiated.GetComponent<Hittable> ().setID (getID ());
			}
		
		}
	}

	// Update is called once per frame
	void Update () {
		cooldown = Mathf.Max(cooldown - Time.deltaTime, 0);
		/*
		RaycastHit2D[] hits = Physics2D.RaycastAll (myRigid.position, new Vector2 (-Mathf.Sin (myRigid.rotation / 180 * Mathf.PI), 
				Mathf.Cos (myRigid.rotation / 180 * Mathf.PI)), 30);
		foreach (RaycastHit2D h in hits)
		*/
		Vector2 myDirection = new Vector2 (-Mathf.Sin (myRigid.rotation / 180 * Mathf.PI), 
			                      Mathf.Cos (myRigid.rotation / 180 * Mathf.PI));
		Collider2D[] hits = Physics2D.OverlapCapsuleAll (myRigid.position + myDirection * (fVision - bVision)/2, new Vector2 (sVision * 2, bVision + fVision), CapsuleDirection2D.Vertical, myRigid.rotation);

		foreach (Collider2D h in hits)
		{
			Hittable target;
			if ((target = h.GetComponentInParent<Hittable> ()) && !target.nontargetable) {
			//if (target = h.collider.GetComponentInParent<Hittable> ()) {
				if (target.getID () == getID ()) {
					continue;
				}
				RaycastHit2D[] obstacleHits = Physics2D.RaycastAll (transform.position, h.transform.position - transform.position, Vector3.Distance (h.transform.position, transform.position));
				bool blocked = false;
				foreach (RaycastHit2D oH in obstacleHits) {
					if (oH.collider.GetComponentInParent<Obstacle> ()) {
						blocked = true;
					}
				}

				if (!blocked) {
					move (myDirection, h);

					attack (myDirection, h);

					break;
				}
			}
		}
	}
}
