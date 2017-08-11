using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Pls make sure this script is attached to the player and you have made a build with at least one build index
public class PlayerDeath : MonoBehaviour
{
    void Update()
    {
        if (transform.position.y < -8)
            Die();
    }

    void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Matt is lame");
    }
}
