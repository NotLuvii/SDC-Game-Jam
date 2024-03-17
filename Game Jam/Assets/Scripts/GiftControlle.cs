using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GiftControlle : MonoBehaviour
{
    // Prefabs for different gifts
    public GameObject redGiftPrefab;
    public GameObject blueGiftPrefab;
    public GameObject purpleGiftPrefab;

    // Cooldowns for each attack
    private float redCooldown = 1f;
    private float blueCooldown = 1f;
    private float combineCooldown = 5f;

    // Timer variables for each attack
    private float redTimer = 0f;
    private float blueTimer = 0f;
    private float combineTimer = 0f;

    // Update is called once per frame
    void Update()
    {
        // Handle red gift attack
        if (Input.GetKeyDown(KeyCode.Q) && redTimer <= 0)
        {
            LaunchGift(redGiftPrefab);
            redTimer = redCooldown;
        }
        // Handle blue gift attack
        else if (Input.GetKeyDown(KeyCode.E) && blueTimer <= 0)
        {
            LaunchGift(blueGiftPrefab);
            blueTimer = blueCooldown;
        }
        // Handle combining gifts into purple gift
        else if (Input.GetKeyDown(KeyCode.R) && combineTimer <= 0)
        {
            LaunchGift(redGiftPrefab);
            LaunchGift(blueGiftPrefab);
            LaunchGift(purpleGiftPrefab);
            combineTimer = combineCooldown;
        }

        // Update cooldown timers
        if (redTimer > 0)
            redTimer -= Time.deltaTime;
        if (blueTimer > 0)
            blueTimer -= Time.deltaTime;
        if (combineTimer > 0)
            combineTimer -= Time.deltaTime;
    }

    // Function to launch a gift
    void LaunchGift(GameObject giftPrefab)
    {
        // Get the direction the camera is facing
        Vector3 launchDirection = Camera.main.transform.forward;

        // Instantiate the gift prefab at the player's position and launch it in the camera's forward direction
        GameObject gift = Instantiate(giftPrefab, transform.position + launchDirection * 2f, Quaternion.identity);
        Rigidbody rb = gift.GetComponent<Rigidbody>();
        rb.velocity = launchDirection * 10f; // Adjust the speed as needed
    }

    // Function to combine gifts into a purple gift
    void CombineGifts()
    {
        GameObject[] redGifts = GameObject.FindGameObjectsWithTag("RedGift");
        GameObject[] blueGifts = GameObject.FindGameObjectsWithTag("BlueGift");

        if (redGifts.Length > 0 && blueGifts.Length > 0)
        {
            // Calculate the average position of red and blue gifts
            Vector3 combinedPosition = Vector3.zero;
            foreach (GameObject redGift in redGifts)
            {
                combinedPosition += redGift.transform.position;
                Destroy(redGift);
            }
            foreach (GameObject blueGift in blueGifts)
            {
                combinedPosition += blueGift.transform.position;
                Destroy(blueGift);
            }
            combinedPosition /= (redGifts.Length + blueGifts.Length);

            // Spawn purple gift at the combined position
            Instantiate(purpleGiftPrefab, combinedPosition, Quaternion.identity);
        }
    }

    
}
    
