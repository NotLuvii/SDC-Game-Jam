using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight : MonoBehaviour
{
    // Prefab for the domain sphere
    public GameObject domainPrefab;

    // Timer for domain expansion readiness
    private float timer = 0f;
    private bool attacksDisabled = true;

    // Cooldown after the sphere is destroyed
    private float cooldownDuration = 30f;
    private float cooldownTimer = 0f;

    // Flag to track if the domain sphere is active
    private bool domainActive = false;

    // Start is called before the first frame update
    void Start()
    {
        // Start the domain expansion process
        ExpandDomain();
    }

    // Update is called once per frame
    void Update()
    {
        if (attacksDisabled)
        {
            // Increment the timer until it reaches the cooldown duration
            if (timer < cooldownDuration)
            {
                timer += Time.deltaTime;
            }
            else
            {
                // After 20 seconds, reset the timer and set attacks to enabled
                attacksDisabled = false;
                timer = 0f;
            }
        }
        else
        {
            // Check if it's time for another domain expansion
            if (cooldownTimer <= 0f)
            {
                PerformRandomDomainExpansion();
                cooldownTimer = cooldownDuration;
            }
            else
            {
                // Update the cooldown timer
                cooldownTimer -= Time.deltaTime;
            }
        }

        Debug.Log(domainActive);
    }

    // Function to expand the domain
    void ExpandDomain()
    {
        // Set the flag to indicate that the domain is active
        domainActive = true;

        // Instantiate the domain sphere at the specified position
        Vector3 domainPosition = new Vector3(-538f, -17f, -40f);
        GameObject domain = Instantiate(domainPrefab, domainPosition, Quaternion.identity);

        // Set the initial scale of the domain sphere
        domain.transform.localScale = new Vector3(1f, 1f, 1f);

        // Destroy the domain after 10 seconds
        StartCoroutine(ScaleDomain(domain));
        //Destroy(domain, 10f);
    }

    // Function to perform a random domain expansion
    void PerformRandomDomainExpansion()
    {
        // Generate a random number between 0 and 1
        float randomValue = UnityEngine.Random.value;

        // If randomValue is less than 0.5, perform another domain expansion
        if (randomValue < 0.5f)
        {
            ExpandDomain();
        }
    }
    System.Collections.IEnumerator ScaleDomain(GameObject domain)
    {

        Debug.Log("Expanding domain");
        Vector3 targetScale = new Vector3(15f, 15f, 15f);
        float duration = 5f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            domain.transform.localScale = Vector3.Lerp(Vector3.one, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Set final scale to ensure accuracy
        domain.transform.localScale = targetScale;

        yield return new WaitForSeconds(10);
        // Destroy the domain sphere after 10 seconds
        Destroy(domain);
        domainActive = false;
    }
    // Function to check if the domain is active
    public bool IsDomainActive()
    {
        return domainActive;
    }
}
