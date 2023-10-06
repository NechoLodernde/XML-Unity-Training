using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;
using System.Xml.Serialization;
using System.IO;
using TMPro;

public class PlayerXMLManager : MonoBehaviour
{
    public static PlayerXMLManager PlayerXMLInstance { get; private set; }

    public PlayerDatabase playerDB;

    [SerializeField] private string objectID;
    private string filepath;

    private void Awake()
    {
        objectID = name + transform.position.ToString();
        filepath = Application.dataPath + @"/StreamingAssets/XML/player_data.xml";

        for (int i = 0; i < FindObjectsOfType<PlayerXMLManager>().Length; i++)
        {
            if (FindObjectsOfType<PlayerXMLManager>()[i] != this)
            {
                if (FindObjectsOfType<PlayerXMLManager>()[i].objectID == objectID)
                {
                    Destroy(gameObject);
                }
            }
        }

        PlayerXMLInstance = this;
    }

    public void SavePlayer()
    {
        XmlDocument xmlDoc = new();
        if (CheckFileLocation())
        {
            xmlDoc.Load(filepath);
            XmlElement elmRoot = xmlDoc.DocumentElement;
            XmlElement elmNew = xmlDoc.CreateElement("PlayerEntry");
            XmlElement playerName = xmlDoc.CreateElement("playerName");
            XmlElement playerRole = xmlDoc.CreateElement("playerRole");
            playerName.InnerText = playerDB.list.ToArray()[0].playerName;
            playerRole.InnerText = playerDB.list.ToArray()[0].playerRole.ToString();

            elmNew.AppendChild(playerName);
            elmNew.AppendChild(playerRole);
            elmRoot.AppendChild(elmNew);

            xmlDoc.Save(filepath);
        }
        else
        {
            InitializeFile();
        }
    }

    public void LoadPlayer()
    {
        XmlDocument xmlDoc = new();
        if (CheckFileLocation())
        {
            xmlDoc.Load(filepath);

            XmlNodeList playerList = xmlDoc.GetElementsByTagName("PlayerEntry");

            foreach(XmlNode playerInfo in playerList)
            {
                XmlNodeList playerContent = playerInfo.ChildNodes;
                PlayerEntry newEntry = new();
                foreach (XmlNode playerItems in playerContent)
                {
                    Debug.Log(playerItems.InnerText);
                    if (playerItems.Name.Equals("playerName"))
                    {
                        newEntry.playerName = playerItems.InnerText;
                    }
                    if (playerItems.Name.Equals("playerRole"))
                    {
                        newEntry.playerRole = GetRole(playerItems.InnerText);
                    }
                }
                playerDB.list.Add(newEntry);
            }
        }
        else
        {
            InitializeFile();
        }
    }

    public void InitializeFile()
    {
        XmlWriterSettings settings = new();
        settings.Encoding = System.Text.Encoding.GetEncoding("UTF-8");
        settings.Indent = true;
        settings.IndentChars = ("    ");
        settings.OmitXmlDeclaration = false;

        XmlWriter writer = XmlWriter.Create(filepath, settings);
        writer.WriteStartDocument();
        writer.WriteStartElement("PlayerDatabase");
        writer.WriteEndElement();
        writer.WriteEndDocument();
        writer.Flush();
    }

    public void ResetData()
    {
        XmlDocument xmlDoc = new();
        if (CheckFileLocation())
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

    private bool CheckFileLocation()
    {
        if (File.Exists(filepath))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public List<string> GetRolesData()
    {
        List<string> list = new();
        // Method 1
        //string[] roles = System.Enum.GetNames(typeof(Roles));
        //for(int i = 0; i < roles.Length; i++)
        //{
        //    Debug.Log(roles[i]);
        //}

        // Method 2
        //foreach(Roles role in System.Enum.GetValues(typeof(Roles))){
        //    Debug.Log(role.ToString());
        //}
        
        foreach(Roles role in System.Enum.GetValues(typeof(Roles)))
        {
            list.Add(role.ToString());
        }

        return list;
    }

    public Roles GetRole(string input)
    {
        Roles baseRole = Roles.Warrior;
        foreach(Roles role in System.Enum.GetValues(typeof(Roles)))
        {
            if (input.Equals(role.ToString()))
            {
                baseRole = role;
                return baseRole;
            }
        }

        return baseRole;
    }
}

[System.Serializable]
public class PlayerEntry
{
    public string playerName;
    public Roles playerRole;
}

[System.Serializable]
public class PlayerDatabase
{
    public List<PlayerEntry> list = new();
}

public enum Roles
{
    Warrior,
    Wizard,
    Healer,
    Hero
}
