using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTracker : MonoBehaviour {
	private Hittable parentHit;
	private SpriteRenderer parentRend;
	private SpriteRenderer myRend;
	private float length;
	private float offsety;
	private float offsetLength;

	// Use this for initialization
	void Start () {
		parentHit = GetComponentInParent<Hittable> ();
		parentRend = GetComponentInParent<SpriteRenderer> ();
		myRend = GetComponent < SpriteRenderer> ();
		length = transform.localScale.x;
		offsety = parentRend.sprite.bounds.size.x + 4 * myRend.sprite.bounds.size.y;
		offsetLength = myRend.sprite.bounds.size.x;
		myRend.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.AngleAxis(0, parentHit.transform.forward);
		transform.position = (Vector2)(parentHit.transform.position + (-transform.up * (offsety)) - transform.right * (offsetLength * (1 - parentHit.getHealth () / parentHit.getMaxHealth ())/2));
		if (parentHit.getHealth() < parentHit.getMaxHealth()) {
			myRend.enabled = true;
			Vector3 resize = transform.localScale;
			resize.x = length * parentHit.getHealth () / parentHit.getMaxHealth ();
			transform.localScale = resize;

			//Debug.Log (parentRend.bounds.size.x / 2f + ", " + 2 * height);
		}


	}
}
