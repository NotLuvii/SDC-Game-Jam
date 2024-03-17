using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public float health;
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            // If health drops below or equal to zero, destroy the GameObject
            //Destroy(gameObject);
        }
    }
}
