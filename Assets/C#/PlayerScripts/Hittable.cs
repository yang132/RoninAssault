using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hittable : MonoBehaviour {
	public float maxHealth;
	public float defense;
	public bool nontargetable;
	private float health;
	private int ID = -1;

	// Use this for initialization
	public virtual void death()
	{
		Destroy (gameObject);
	}

	public void setHealth(float h)
	{
		health = h;
	}

	public void setHealth()
	{
		health = maxHealth;
	}

	public float getHealth()
	{
		return health;
	}

	public float getMaxHealth()
	{
		return maxHealth;
	}

	public virtual float damage(float d)
	{
		float recoil = Mathf.Max (0, health);
		health -= Mathf.Max(0, d-defense);

		if (health <= 0) {
			death ();
		}
		return recoil;

	}
		

	public int getID()
	{
		return ID;
	}
	public void setID(int i)
	{
		ID = i;
	}

}
