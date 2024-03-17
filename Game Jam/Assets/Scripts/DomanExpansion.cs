using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DomanExpansion : MonoBehaviour
{
    // Prefab for the domain sphere
    public GameObject domainPrefab;

    // Cooldown after the sphere is destroyed
    private float cooldownDuration = 25f;
    private float cooldownTimer = 0f;
    public AudioClip[] domainExpansionSounds;
    private float timer = 0f;
    private bool attacksDisabled = true;

    void Start()
    {
        // Add an AudioSource component to the GameObject and set the audio clip
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (attacksDisabled)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                Debug.Log((int)timer);
            }
            else
            {
                Debug.Log("Domain expansion ready");
                timer = 10;
                attacksDisabled = false;
            }

            
        }
        else
        {
            // Check for input to trigger domain expansion
            if (Input.GetKeyDown(KeyCode.G) && cooldownTimer <= 0)
            {
                ExpandDomain();
                cooldownTimer = cooldownDuration;
            }

            // Update cooldown timer
            if (cooldownTimer > 0)
                cooldownTimer -= Time.deltaTime;
        }
        
    }

    // Function to expand the domain
    void ExpandDomain()
    {
        for (int i = 0; i < domainExpansionSounds.Length; i++)
        {
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.PlayOneShot(domainExpansionSounds[i]);
        }
        // Instantiate the domain sphere at the specified position
        Vector3 domainPosition = new Vector3(-576f, -69f, -13f);
        GameObject domain = Instantiate(domainPrefab, domainPosition, Quaternion.identity);

        // Set the size of the domain sphere
        //domain.transform.localScale = new Vector3(750f, 750f, 750f);

        // Start scaling up the domain sphere until it reaches scale 10
        StartCoroutine(ScaleDomain(domain));
    }

    // Coroutine to scale up the domain sphere
    System.Collections.IEnumerator ScaleDomain(GameObject domain)
    {

        Debug.Log("Expanding domain");
        Vector3 targetScale = new Vector3(750f, 750f, 750f);
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

        // Destroy the domain sphere after 10 seconds
        Destroy(domain, 10f);
    }
}
