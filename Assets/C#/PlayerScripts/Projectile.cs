using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Hittable {
	public float life;
	public float duration;
	private bool started;
	private SpriteRenderer render;

	public float damageScale;
	public float initialVel;
	public float velocity;
	public float knockback;
	Rigidbody2D myRigid;

	// Use this for initialization
	void Start () {
		started = false;
		render = this.GetComponent<SpriteRenderer> ();
		render.enabled = true;

		myRigid = this.GetComponent<Rigidbody2D> ();
		this.enabled = true;
		myRigid.velocity = initialVel *(new Vector2 (-Mathf.Sin (myRigid.rotation /180 * Mathf.PI),Mathf.Cos (myRigid.rotation /180 * Mathf.PI)));
		//Debug.Log (initialVel);
		setHealth ();
	}

	// Update is called once per frame
	protected void Update () {
		if (started == false && render.enabled)
		{
			started = true;
			StartCoroutine (delay (life));

		}

		myRigid.velocity = Mathf.Max(velocity, myRigid.velocity.magnitude) *(new Vector2 (-Mathf.Sin (myRigid.rotation /180 * Mathf.PI),Mathf.Cos (myRigid.rotation /180 * Mathf.PI)));
		//Debug.Log (Mathf.Sin (myRigid.rotation /180 * Mathf.PI) + ", " + Mathf.Cos (myRigid.rotation /180 * Mathf.PI));
	}

	IEnumerator delay(float sec)
	{
		yield return new WaitForSeconds(sec);
		//this.GetComponent<SpriteRenderer> ().enabled = false;
		started = false;
		Destroy (gameObject);
	} 

	void OnTriggerEnter2D(Collider2D other)
	{
		Hittable hit;
		if ((hit = other.GetComponentInParent<Hittable> ()) && this.getID() != hit.getID()) {
			//Debug.Log ("HIT");
			float recoil = -1;
			recoil = other.GetComponentInParent<Hittable> ().damage (damageScale);
			Rigidbody2D oRigid;
			if (oRigid = other.GetComponentInParent<Rigidbody2D> ()) {
				oRigid.AddForce(knockback * (-GetComponent<Rigidbody2D>().position + oRigid.position));
			}
			damage(recoil);

			Debug.Log (recoil);
		}


	}


}
