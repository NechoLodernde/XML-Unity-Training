using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerEdit : MonoBehaviour
{
    public TMP_Text playerNameField, playerRoleField;

    private readonly string playerEditSceneName = "PlayerEditScene";

    public void GoToEditPlayerScene()
    {
        PlayerXMLManager.PlayerXMLInstance.playerDB.list.Clear();
        PlayerEntry modifyEntry = new();
        modifyEntry.playerName = playerNameField.text;
        modifyEntry.playerRole = PlayerXMLManager.PlayerXMLInstance.GetRole(playerRoleField.text);
        PlayerXMLManager.PlayerXMLInstance.playerDB.list.Add(modifyEntry);
        PlayerXMLManager.PlayerXMLInstance.SetPrevPlayerName(playerNameField.text);
        SceneManager.LoadScene(playerEditSceneName);
    }
}
