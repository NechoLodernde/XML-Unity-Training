using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextDisplay : MonoBehaviour
{
    public TMP_Text displayText;

    // Start is called before the first frame update
    void Start()
    {
        TextXMLManager.TextXMLInstance.LoadTexts();
        Display();
    }

    public void Display()
    {
        displayText.text = "";

        foreach (TextXMLManager.TextEntry textEntry in TextXMLManager.TextXMLInstance.textDB.list)
        {
            displayText.text +=  textEntry.text + " ";
        }
    }
}
