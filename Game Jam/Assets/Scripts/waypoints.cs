using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    // Reference to the player GameObject (assuming it has a collider and Rigidbody)
    public GameObject player;

    // Reference to the object to be destroyed
    public GameObject objectToDestroy;
    public GameObject objectToSpawn;
    public Animator kidAnimator; // Reference to the kid's animator controller
    private bool destroyed = false;

    // Update is called once per frame
    void Update()
    {
        // Check for collision with the player and if the player presses the "E" key
        if (Input.GetKeyDown(KeyCode.E) && player != null && objectToDestroy != null)
        {
            // Check if the player is colliding with the object
            if (player.GetComponent<Collider>().bounds.Intersects(objectToDestroy.GetComponent<Collider>().bounds))
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

                    // Trigger kid's action from the animator controller
                    if (kidAnimator != null)
                    {
                        kidAnimator.SetTrigger("DanceTrigger"); // Assuming you have a trigger parameter named "ActionTrigger" in the animator controller
                    }
                }
            }
        }
    }
}