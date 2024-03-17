using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string game;
    public string about;
    public void ChangeScene()
    {
        // Load the specified scene
        SceneManager.LoadScene("PreGame");
    }

    public void loadAboutScene()
    {
        SceneManager.LoadScene(about);
    }
}
