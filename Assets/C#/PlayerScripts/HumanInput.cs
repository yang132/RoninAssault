using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanInput : Hittable {
	public Transform SpawnedSlash1;

	private Rigidbody2D myRigid;
	private float angle;
	private Transform swordAttack;
	public float attackRate;
	private float cooldown;
	public Transform DeathMenu;
	//private Transform swordSlash;

	// Use this for initialization
	void Start () {
		myRigid = this.GetComponent<Rigidbody2D> ();
		swordAttack = this.transform.GetChild (0);
		swordAttack.GetComponent<SpriteRenderer> ().enabled = false;
		swordAttack.GetComponent<SpriteRenderer>().enabled = false;

		//swordSlash = this.transform.GetChild (1);
		//swordSlash.GetComponent<SpriteRenderer> ().enabled = false;
		setID(1);
		setHealth ();
		cooldown = 0;

		Debug.Log ("start");
	}

	// Update is called once per frame
	void Update () {
		cooldown = Mathf.Max(cooldown - Time.deltaTime, 0);
		myRigid.AddForce (1500 * Time.deltaTime *(new Vector2 (Input.GetAxis("Horizontal"),Input.GetAxis("Vertical")))); //movement


		Vector2 mousePos = new Vector2 (); //Point towards mouse
		mousePos.x = Input.mousePosition.x;
		mousePos.y = Input.mousePosition.y;
		Vector3 p = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));
		float xdiff = (p.x - myRigid.position.x);
		float ydiff = (p.y - myRigid.position.y);
		angle = -Mathf.Atan (xdiff / ydiff) * 180 / Mathf.PI;
		float offset = Mathf.Sign(angle)* (1 - Mathf.Sign(Mathf.Asin(ydiff/Mathf.Sqrt(xdiff * xdiff + ydiff * ydiff))))/2 * 180;
		myRigid.rotation = angle - offset;

		if (Input.GetMouseButtonDown (0) && cooldown <= 0) {
			swordAttack.GetComponent<SpriteRenderer>().enabled = true;
			//swordSlash.GetComponent<SpriteRenderer>().enabled = true;
			cooldown = attackRate;

			xdiff = myRigid.position.x - Mathf.Sin (myRigid.rotation /180 * Mathf.PI) * .7755346f;
			ydiff = myRigid.position.y+Mathf.Cos (myRigid.rotation/180 * Mathf.PI) * .7755346f;

			//.7755436
			GameObject instantiated;
			instantiated = Instantiate (SpawnedSlash1, new Vector2 (xdiff, ydiff), Quaternion.AngleAxis(myRigid.rotation, Vector3.forward)).gameObject;
			instantiated.GetComponent<Hittable> ().setID (getID ());
			//Physics2D.IgnoreCollision (instantiated.GetComponent<Collider2D> (), this.gameObject.GetComponent<Collider2D>());

			//Debug.Log ("sin: " + Mathf.Sin (myRigid.rotation /180 * Mathf.PI) + ", cos: " + Mathf.Cos (myRigid.rotation/180 * Mathf.PI) + ", rot: " +myRigid.rotation /180 * Mathf.PI);
		} else if (Input.GetMouseButtonUp (0)) {
			swordAttack.GetComponent<SpriteRenderer>().enabled = false;
			//swordSlash.GetComponent<SpriteRenderer>().enabled = false;
		}


	}

	public override void death()
	{
		Instantiate (DeathMenu, Vector2.zero, Quaternion.identity);

		Destroy (gameObject);
	}

}
