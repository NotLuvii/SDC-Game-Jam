using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypoints : MonoBehaviour
{

    // Reference to the player GameObject (assuming it has a collider and Rigidbody)
    public GameObject player;

    // Reference to the object to be destroyed
    public GameObject objectToDestroy;
    public GameObject objectToSpawn;
    private bool destroyed = false;
    // Update is called once per frame
    void Update()
    {
        // Check for collision with the player and if the player presses the "E" key
        if (Input.GetKeyDown(KeyCode.E) && player != null && objectToDestroy != null)
        {
            // Check if the player is colliding with the object
            if(player.GetComponent<Collider>().bounds.Intersects(objectToDestroy.GetComponent<Collider>().bounds))
            {
                // Destroy the object if it hasn't been destroyed already
                if (!destroyed)
                {
                    // Destroy the object
                    Destroy(objectToDestroy);

                    // Spawn a new object at the same location
                    if (objectToSpawn != null)
                    {
                        Instantiate(objectToSpawn, objectToDestroy.transform.position, objectToDestroy.transform.rotation);
                    }

                    // Set the flag to true
                    destroyed = true;
                }
            }
        }


    }
}
