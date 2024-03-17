using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    // Prefabs for different gifts
    public GameObject Boss;
    public GameObject redGiftPrefab;
    public GameObject blueGiftPrefab;
    public GameObject purpleGiftPrefab;
    public GameObject capsule;

    // Cooldowns for each attack
    private float redCooldown = 4f; // Adjusted cooldown for the boss
    private float blueCooldown = 4f; // Adjusted cooldown for the boss

    // Timer variables for each attack
    private float redTimer = 0f;
    private float blueTimer = 2f;

    // Number of hits the boss can take before dying
    private int hitsToDie = 7;
    private int currentHits = 0;

    // Update is called once per frame
    void Update()
    {
        // Boss attacks on a timed basis
        if (redTimer <= 0)
        {
            LaunchGift(redGiftPrefab);
            redTimer = redCooldown;
        }
        else
        {
            redTimer -= Time.deltaTime;
        }

        if (blueTimer <= 0)
        {
            LaunchGift(blueGiftPrefab);
            blueTimer = blueCooldown;
        }
        else
        {
            blueTimer -= Time.deltaTime;
        }
    }

    // Function to launch a gift
    void LaunchGift(GameObject giftPrefab)
    {
        // Get the direction towards the capsule object
        Vector3 launchDirection = (capsule.transform.position - transform.position).normalized;

        // Instantiate the gift prefab at the boss's position and launch it in the capsule's direction
        GameObject gift = Instantiate(giftPrefab, transform.position + launchDirection * 2f, Quaternion.identity);
        Rigidbody rb = gift.GetComponent<Rigidbody>();
        rb.velocity = launchDirection * 10f; // Adjust the speed as needed
    }

    // Function to handle boss getting hit
    public void TakeHit()
    {
        currentHits++;
        if (currentHits >= hitsToDie)
        {
            Die();
        }
    }

    // Function to handle boss's death
    void Die()
    {
        // Implement boss death logic here
        Debug.Log("Boss is defeated!");
        Destroy(Boss); // Destroy the boss object
    }

    // OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            TakeHit(); // Call TakeHit() when the boss collides with a projectile
        }
    }
}
