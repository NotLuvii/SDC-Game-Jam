using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextSliderThingy : MonoBehaviour
{
    public TMP_Text[] tmproObjects;
    private int currentIndex = 0;

    void Start()
    {
        StartCoroutine(ShowSequentially());
    }

    IEnumerator ShowSequentially()
    {
        yield return new WaitForSeconds(0.5f); // Initial wait time before showing the first TMPro object

        while (currentIndex < tmproObjects.Length)
        {
            tmproObjects[currentIndex].gameObject.SetActive(true);
            yield return new WaitForSeconds(3f); // Wait for 2 seconds before showing the next TMPro object
            tmproObjects[currentIndex].gameObject.SetActive(false);
            currentIndex++;

        }
        SceneManager.LoadScene("SampleScene");
    }
}
