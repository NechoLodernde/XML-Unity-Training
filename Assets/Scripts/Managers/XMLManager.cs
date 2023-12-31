using System.Collections;
using System.Collections.Generic;   // Lets us use lists
using UnityEngine;

using System.Xml;                   // Basic xml attributes
using System.Xml.Serialization;     // Access xml serializer
using System.IO;                    // File management

public class XMLManager : MonoBehaviour
{
    // Creating static object that can be referenced in other script
    public static XMLManager XMLInstance { get; private set; }

    // List of items
    public ItemDatabase itemDB;

    // Create object ID for multi instance checking
    [SerializeField] private string objectID;

    // Method that will be the first to run before Start Method
    private void Awake()
    {
        // Added object ID with name + object position to string
        objectID = name + transform.position.ToString();
        // Create a for loop for iterating all object of same type
        for (int i = 0; i < FindObjectsByType<XMLManager>(FindObjectsSortMode.None).Length; i++)
        {
            // If the object is not equal to this
            if (FindObjectsByType<XMLManager>(FindObjectsSortMode.None)[i] != this)
            {
                // If the object have the same ID, then destroy the new instance
                if (FindObjectsByType<XMLManager>(FindObjectsSortMode.None)[i].objectID == objectID)
                {
                    Destroy(gameObject);
                }
            }
        }

        // Set the instance to this object
        XMLInstance = this;
    }

    // Save function to XML
    public void SaveItems()
    {
        // Open a new xml file
        // Create xml serializer with the type to serialize
        XmlSerializer serializer = new(typeof(ItemDatabase));

        // Method 1
        // FileStream for the path and FileMode to access/modify the file
        // FileStream stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/item_data.xml", FileMode.Create);
        // Serialize the item database with chosen stream
        // serializer.Serialize(stream, itemDB);
        // Close the stream after use
        // stream.Close();

        // Method 2
        // Create encoding variable and set the Text Encoding format
        var encoding = System.Text.Encoding.GetEncoding("UTF-8");
        // Create StreamWriter to write into the file with the encoding type
        StreamWriter stream = new(Application.dataPath + "/StreamingFiles/XML/item_data.xml", false, encoding);
        // Serialize the item database with chosen stream
        serializer.Serialize(stream, itemDB);
        // Close the stream after use
        stream.Close();

        // Method 3
        // FileStream fileCreate = new(Application.dataPath + "/StreamingFiles/XML/item_data.xml", FileMode.Create);
        // var encoded = System.Text.Encoding.GetEncoding("UTF-8");
        // StreamWriter createStream = new(fileCreate, encoded);
        // serializer.Serialize(createStream, itemDB);
        // createStream.Close();
    }

    // Load function
    public void LoadItems()
    {
        // Access an xml file
        // Create xml serializer with the type to serialize
        XmlSerializer serializer = new XmlSerializer(typeof(ItemDatabase));

        // Method 1
        // FileStream for the path and FileMode to access/modify the file
        // FileStream stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/item_data.xml", FileMode.Open);
        // De-Serialize the item database
        // itemDB = serializer.Deserialize(stream) as ItemDatabase;
        // Close the stream after use
        // stream.Close();

        // Method 2
        // Create the system path for the stream
        string path = Application.dataPath + "/StreamingFiles/XML/item_data.xml";
        // StreamReader to read the file from the designated path
        StreamReader stream = new(path);
        // De-serialize the item database from stream
        itemDB = serializer.Deserialize(stream) as ItemDatabase;
        // Close the stream after use
        stream.Close();
    }
}

// Allows class to be seen on Editor Inspector
// Is what going to be populating the list
[System.Serializable]
public class ItemEntry
{
    public string itemName;
    public Material itemMaterial;
    public int itemValue;
}

// What's gonna be made into files
[System.Serializable]
public class ItemDatabase
{
    // Allows you to modify the list name on xml file, not recommended to use spaces in between words
    [XmlArray("CombatEquipment")]
    // Create List with the type of Item Entry that will contain the items
    public List<ItemEntry> list = new();
}

// Create enum for materials available
public enum Material
{
    Wood,
    Copper,
    Iron,
    Steel,
    Gold
};