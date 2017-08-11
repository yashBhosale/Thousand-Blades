
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot: MonoBehaviour
{
    private Collider2D hero;
    // set in the inspector
    public Material slingShotMaterial;
    // set in the inspector
    public Material originMaterial;
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
            rend.material = slingShotMaterial;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            rend.material = originMaterial;
            hero = null;
        }
    }
}