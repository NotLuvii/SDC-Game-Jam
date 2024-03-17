using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextHoverEffect : MonoBehaviour
{
    public float hoverRange = 30f; // Adjust this to control the range of hovering
    public float hoverSpeed = 10f;   // Adjust this to control the speed of hovering

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        // Calculate vertical movement using sine function for a smooth hover effect
        float yOffset = Mathf.Sin(Time.time * hoverSpeed) * hoverRange*50f;

        // Apply the vertical movement to the text's position
        transform.position = initialPosition + new Vector3(0f, yOffset, 0f);
    }
}

