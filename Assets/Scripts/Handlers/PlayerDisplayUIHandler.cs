using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDisplayUIHandler : MonoBehaviour
{
    private readonly string mainMenuSceneName = "MainMenuScene";
    private readonly string addPlayerSceneName = "PlayerAddScene";

    private void Start()
    {
        PlayerXMLManager.PlayerXMLInstance.playerDB.list.Clear();
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void GoToPlayerAdd()
    {
        SceneManager.LoadScene(addPlayerSceneName);
    }
}
