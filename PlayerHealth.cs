using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    float playerHealth = 100;
    private PlayerDeath player;

    private void Start()
    {
        player = GetComponent<PlayerDeath>();
    }

    private void Update()
    {
        if(playerHealth <= 0)
        {
            player.Die();
        }
    }

    public void decrementHealth(float damage)
    {
        playerHealth -= damage;
        Debug.Log(playerHealth);
    }

    public void incrementHealth(float addedHealth)
    {
        playerHealth += addedHealth;
    }
}
