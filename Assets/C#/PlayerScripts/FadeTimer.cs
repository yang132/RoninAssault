using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeTimer : MonoBehaviour {
	public float life;
	public float duration;
	private bool started;
	private SpriteRenderer render;

	// Use this for initialization
	void Start () {
		started = false;
		render = this.GetComponent<SpriteRenderer> ();
		render.enabled = true;
		Start2 ();
	}

	protected virtual void Start2()
	{
	}

	protected virtual void Update2()
	{
		Debug.Log ("Update2Base");
	}

	void Update() {
		if (started == false && render.enabled)
		{
			started = true;
			StartCoroutine (delay (life));

		}
		Update2 ();
	}

	IEnumerator delay(float sec)
	{
		yield return new WaitForSeconds(sec);
		//this.GetComponent<SpriteRenderer> ().enabled = false;
		started = false;
		Destroy (gameObject);
	}
}