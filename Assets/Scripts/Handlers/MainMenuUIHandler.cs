using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuUIHandler : MonoBehaviour
{
    private readonly string itemStoreSceneName = "ItemStoreScene";
    private readonly string textDisplaySceneName = "TextDisplayScene";

    public void GoToItemStore()
    {
        SceneManager.LoadScene(itemStoreSceneName);
    }

    public void GoToTextDisplay()
    {
        SceneManager.LoadScene(textDisplaySceneName);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
