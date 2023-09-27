using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class TextXMLManager : MonoBehaviour
{
    public static TextXMLManager TextXMLInstance { get; private set; }

    public TextDatabase textDB;

    [SerializeField] private string objectID;

    private void Awake()
    {
        objectID = name + transform.position.ToString();
        
        for(int i = 0; i < Object.FindObjectsOfType<TextXMLManager>().Length; i++)
        {
            if (Object.FindObjectsOfType<TextXMLManager>()[i] != this)
            {
                if(Object.FindObjectsOfType<TextXMLManager>()[i].objectID == objectID)
                {
                    Destroy(gameObject);
                }
            }
        }

        TextXMLInstance = this;
    }

    private void Start()
    {
        TextEntry newText = new();
        newText.text = "Hai";
        TextXMLInstance.textDB.list.Add(newText);
    }

    [System.Serializable]
    public class TextEntry
    {
        public string text;
    }

    [System.Serializable]
    public class TextDatabase
    {
        public List<TextEntry> list = new();
    }
}
