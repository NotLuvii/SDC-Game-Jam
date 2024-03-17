using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    public GameObject objectPrefab; // Prefab of the object to be launched
    public float launchSpeed = 5f; // Initial speed of the launched object
    public float damageAmount = 2f; // Damage inflicted on the player
    public float doubleDamageMultiplier = 2f; // Multiplier for damage if boss domain is active
    public float launchInterval = 2f; // Interval between launches

    private Transform playerTransform; // Reference to the player's transform
    private bool BossFightActive = false; // Flag to check if boss domain is active

    private void Start()
    {
        // Find the player GameObject and get its transform
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player not found in the scene!");
        }

        // Start launching objects every launchInterval seconds
        InvokeRepeating("LaunchObjectTowardsPlayer", 0f, launchInterval);

        // Check if the boss domain is active
        BossFight BossFight = FindObjectOfType<BossFight>();
        if (BossFight != null)
        {
            BossFightActive = BossFight.IsDomainActive();
        }
    }

    private void LaunchObjectTowardsPlayer()
    {
        if (playerTransform != null)
        {
            // Calculate the direction from the current position to the player's position
            Vector3 launchDirection = (playerTransform.position - transform.position).normalized;

            // Instantiate the object prefab
            GameObject launchedObject = Instantiate(objectPrefab, transform.position, Quaternion.identity);

            // Get the Rigidbody component of the launched object
            Rigidbody rb = launchedObject.GetComponent<Rigidbody>();

            // Set the velocity of the launched object
            if (rb != null)
            {
                rb.velocity = launchDirection * launchSpeed;
            }
            else
            {
                Debug.LogError("Rigidbody component not found in the object prefab!");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object collides with the player
        if (other.CompareTag("Player"))
        {
            // Get the health script of the player
            Health Health = other.GetComponent<Health>();
            if (Health != null)
            {
                // Calculate damage based on boss domain status
                float calculatedDamage = BossFightActive ? damageAmount * doubleDamageMultiplier : damageAmount;
                Health.TakeDamage(calculatedDamage);
            }
            else
            {
                Debug.LogError("Player health script not found!");
            }

            // Destroy the object after collision with the player
            Destroy(gameObject);
        }
    }


}
