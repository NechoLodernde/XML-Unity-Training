using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartupUIHandler : MonoBehaviour
{
    private readonly string mainMenuSceneName = "MainMenuScene";

    private void Start()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
