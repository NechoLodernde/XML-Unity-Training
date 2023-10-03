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

    /*
    private void Start()
    {
        TextEntry newText = new();
        newText.text = "Hai";
        TextXMLInstance.textDB.list.Add(newText);
    }
    */

    public void SaveTexts()
    {
        // Method 1 (There's problem with appending data to xml)
        //XmlSerializer serializer = new(typeof(TextDatabase));

        //var encoding = System.Text.Encoding.GetEncoding("UTF-8");
        //StreamWriter stream = new(Application.dataPath + "/StreamingFiles/XML/text_data.xml", false, encoding);
        //serializer.Serialize(stream, textDB);
        //stream.Close();

        // Method 2 (This is the current solution with appending data to xml)
        // Create filepath for the xml file
        string filepath = Application.dataPath + @"/StreamingAssets/XML/text_data.xml";
        // Create new XML Document variable to access and modify
        XmlDocument xmlDoc = new();
        // Check if the File exist
        if (File.Exists(filepath))
        {
            // Load the content from filepath to the xml doc
            xmlDoc.Load(filepath);

            // Get the document root 
            XmlElement elmRoot = xmlDoc.DocumentElement;
            // This will remove all element in root
            //elmRoot.RemoveAll();

            // Create new child element from root element
            XmlElement elmNew = xmlDoc.CreateElement("TextEntry");
            // Create new child element from previous element
            XmlElement textEntry = xmlDoc.CreateElement("text");
            // Set the inner text of current child element
            textEntry.InnerText = textDB.list.ToArray()[0].text;

            // Append the child to the previous element
            elmNew.AppendChild(textEntry);
            // Append the child to the root element
            elmRoot.AppendChild(elmNew);

            // Save the xml doc to the filepath
            xmlDoc.Save(filepath);
        }
        else
        {
            InitializeFile();
        }
        
    }

    public void LoadTexts()
    {
        // Method 1 (There's problem with appending data to xml)
        //XmlSerializer serializer = new(typeof(TextDatabase));

        //string path = Application.dataPath + "/StreamingFiles/XML/text_data.xml";
        //StreamReader stream = new(path);
        //textDB = serializer.Deserialize(stream) as TextDatabase;
        //stream.Close();

        string filepath = Application.dataPath + @"/StreamingAssets/XML/text_data.xml";
        XmlDocument xmlDoc = new();

        if (File.Exists(filepath))
        {
            xmlDoc.Load(filepath);

            XmlNodeList textList = xmlDoc.GetElementsByTagName("TextEntry");

            foreach (XmlNode textInfo in textList)
            {
                XmlNodeList textContent = textInfo.ChildNodes;

                foreach (XmlNode textItems in textContent)
                {
                    if (textItems.Name.Equals("text"))
                    {
                        TextEntry loadedText = new();
                        loadedText.text = textItems.InnerText;
                        textDB.list.Add(loadedText);
                    }
                }
            }
        }
        else
        {
            InitializeFile();
        }
    }

    public void ResetData()
    {
        string filepath = Application.dataPath + @"/StreamingAssets/XML/text_data.xml";
        XmlDocument xmlDoc = new();
        if (File.Exists(filepath))
        {
            xmlDoc.Load(filepath);
            XmlElement elmRoot = xmlDoc.DocumentElement;
            elmRoot.RemoveAll();
            xmlDoc.Save(filepath);
        }
        else
        {
            InitializeFile();
        }
    }

    public void InitializeFile()
    {
        string filepath = Application.dataPath + @"/StreamingAssets/XML/text_data.xml";

        XmlWriterSettings settings = new();
        settings.Encoding = System.Text.Encoding.GetEncoding("UTF-8");
        settings.Indent = true;
        settings.IndentChars = ("    ");
        settings.OmitXmlDeclaration = false;

        XmlWriter writer = XmlWriter.Create(filepath, settings);
        writer.WriteStartDocument();
        writer.WriteStartElement("TextDatabase");
        writer.WriteEndElement();
        writer.WriteEndDocument();
        writer.Flush();
    }

    [System.Serializable]
    public class TextEntry
    {
        public string text;
    }

    [System.Serializable]
    public class TextDatabase
    {
        [XmlArray("ListOfTexts")]
        public List<TextEntry> list = new();
    }
}
