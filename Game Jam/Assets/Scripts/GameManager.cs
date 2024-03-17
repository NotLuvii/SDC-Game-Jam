using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Reference to the parent object whose children count we want to display
    public Transform parentObject;

    // Reference to the TextMeshPro text component
    public TMP_Text textComponent;
    public TMP_Text timerText;
    public float timerDuration = 60f;
    private float timer;

    void Awake()
    {
        textComponent.outlineWidth = 0.1f;
        textComponent.outlineColor = new Color32(0,0,0, 255);

        timerText.outlineWidth = 0.1f;
        timerText.outlineColor = new Color32(0,0,0, 255);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the parentObject and textComponent are assigned
        if (parentObject != null && textComponent != null)
        {
            // Get the number of children the parentObject has
            int childCount = parentObject.childCount;

            // Update the text to display the child count
            textComponent.text = "Gifts left: " + childCount.ToString();

            if (childCount == 0)
            {
                SceneManager.LoadScene("win");
            }
        }
        else
        {
            // If the parentObject or textComponent is not assigned, log a warning
            Debug.LogWarning("ParentObject or TextComponent not assigned!");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        timer -= Time.deltaTime;

        
        timerText.text = "Time remaining: " + (int)timer;
        if (timer <= 0)
        {
            SceneManager.LoadScene("PostGame");
        }
    }

    // Initialize timer when the script starts
    private void Start()
    {
        timer = timerDuration;
    }

    // Convert the timer value to an integer before displaying it
    private void LateUpdate()
    {
        //timerText.text = timerText.text.Replace("{timer}", Mathf.FloorToInt(timer).ToString());
    }
}
