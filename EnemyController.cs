using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
	private Collider2D hero;

	public void Update()
	{
		if(Input.GetKeyDown(KeyCode.E))
		if(hero != null)
			hero.gameObject.GetComponent<Player>().slingShot(transform.position);
	}
	public  void OnTriggerEnter2D(Collider2D other)
	{
		hero = other;
	}

	void OnTriggerExit2D(Collider2D other)
	{
		hero = null;
	}
}
