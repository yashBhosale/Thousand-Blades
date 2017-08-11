
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot: MonoBehaviour
{
    private Collider2D hero;
  
    private Renderer rend;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            if (hero != null)
                hero.gameObject.GetComponent<Player>().slingShot(transform.position);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            hero = other;
            rend = GetComponent<Renderer>();
            rend.material.color = Color.blue;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            rend.material.color = Color.white;
            hero = null;
        }
    }
}