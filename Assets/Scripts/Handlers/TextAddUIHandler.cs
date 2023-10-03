using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TextAddUIHandler : MonoBehaviour
{
    public TMP_InputField textInput;
    public GameObject notifObject;

    private readonly string textDisplaySceneName = "TextDisplayScene";
    private readonly float notifTimer = 4.0f;

    public void GoToTextDisplay()
    {
        SceneManager.LoadScene(textDisplaySceneName);
    }

    public void SubmitText()
    {
        string tInput = textInput.text.ToString();
        TMP_Text notifText = notifObject.GetComponentInChildren<TMP_Text>();
        if(tInput.Equals("") || tInput.Length < 2)
        {
            notifObject.SetActive(true);
            notifText.text = "Input can't be empty or less than 2 letters";
            textInput.text = "";
            StartCoroutine(SpawnNotif());
        }
        else if (tInput.Length >= 2)
        {
            TextXMLManager.TextXMLInstance.textDB.list.Clear();
            TextXMLManager.TextEntry newEntry = new();
            newEntry.text = tInput;
            TextXMLManager.TextXMLInstance.textDB.list.Add(newEntry);
            TextXMLManager.TextXMLInstance.SaveTexts();
            GoToTextDisplay();
        }
    }

    IEnumerator SpawnNotif()
    {
        yield return new WaitForSeconds(notifTimer);
        notifObject.SetActive(false);
    }
}
