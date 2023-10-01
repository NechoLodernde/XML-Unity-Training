using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TextDisplayUIHandler : MonoBehaviour
{
    private readonly string mainMenuSceneName = "MainMenuScene";
    private readonly string textAddSceneName = "TextAddScene";

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void GoToTextAdd()
    {
        SceneManager.LoadScene(textAddSceneName);
    }
}
