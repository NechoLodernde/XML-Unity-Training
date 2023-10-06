using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerAddUIHandler : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public TMP_Dropdown roleInputField;
    public GameObject notifObject;

    private readonly string playerDisplaySceneName = "PlayerDisplayScene";
    private readonly float notifTimer = 5.0f;

    private void Start()
    {
        var roleDropdown = roleInputField.transform.GetComponent<TMP_Dropdown>();
        List<string> roleOption;
        roleOption = PlayerXMLManager.PlayerXMLInstance.GetRolesData();
        roleDropdown.AddOptions(roleOption);
    }

    public void GoToPlayerDisplay()
    {
        SceneManager.LoadScene(playerDisplaySceneName);
    }

    public void SubmitData()
    {
        var inputDropDown = roleInputField.transform.GetComponent<TMP_Dropdown>();
        int index = inputDropDown.value;
        List<TMP_Dropdown.OptionData> roleOptions = inputDropDown.options;
        string playerName = nameInputField.text.ToString();
        string playerRole = roleOptions[index].text.ToString();
        TMP_Text notifText = notifObject.GetComponentInChildren<TMP_Text>();
        if(playerName.Equals("") || playerName.Length < 3)
        {
            notifObject.SetActive(true);
            notifText.text = "Name can't be empty or less than 3 letters";
            nameInputField.text = "";
            StartCoroutine(SpawnNotif());
        } else if(playerName.Length >= 3)
        {
            PlayerXMLManager.PlayerXMLInstance.playerDB.list.Clear();
            PlayerEntry newEntry = new();
            newEntry.playerName = playerName;
            newEntry.playerRole = PlayerXMLManager.PlayerXMLInstance.GetRole(playerRole);
            PlayerXMLManager.PlayerXMLInstance.playerDB.list.Add(newEntry);
            PlayerXMLManager.PlayerXMLInstance.SavePlayer();
            GoToPlayerDisplay();
        }
    }

    IEnumerator SpawnNotif()
    {
        yield return new WaitForSeconds(notifTimer);
        notifObject.SetActive(false);
    }
}
