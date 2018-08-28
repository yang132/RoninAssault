using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EventScript : MonoBehaviour {

	// Use this for initialization
	public void printMessage()
	{
		Debug.Log ("Hello World");
	}

	public void openScene(string name)
	{
		SceneManager.LoadScene (name);
	}

	public void nextScene()
	{
		GameObject c = GameObject.Find ("DebriefMenu");
		if (c == null) {
			Debug.Log ("Error: DebriefMenu not found");
		} else {
			SceneManager.LoadScene (c.GetComponent<LevelInfo>().nextLevel);
		}
	}

	public void reloadScene()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}

	public void closeCanvas(Canvas c)
	{
		
		c.enabled = false;
		GameObject player = GameObject.Find ("Ronin");
		player.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.None;

		//GameObject.Find ("Ronin").GetComponent<HumanInput> ().enabled = true;
	}

	// Update is called once per frame

}
