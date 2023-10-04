using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDisplayUIHandler : MonoBehaviour
{
    private readonly string mainMenuSceneName = "MainMenuScene";
    private readonly string addPlayerSceneName = "PlayerAddScene";

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void GoToPlayerAdd()
    {
        SceneManager.LoadScene(addPlayerSceneName);
    }
}
