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

    // Create object ID for multi instance checking
    [SerializeField] private string objectID;

    // Method that will be the first to run before Start Method
    private void Awake()
    {
        // Added object ID with name + object position to string
        objectID = name + transform.position.ToString();
        // Create a for loop for iterating all object of same type
        for (int i = 0; i < Object.FindObjectsOfType<XMLManager>().Length; i++)
        {
            // If the object is not equal to this
            if (Object.FindObjectsOfType<XMLManager>()[i] != this)
            {
                // If the object have the same ID, then destroy the new instance
                if (Object.FindObjectsOfType<XMLManager>()[i].objectID == objectID)
                {
                    Destroy(gameObject);
                }
            }
        }

        // Set the instance to this object
        XMLInstance = this;
    }

    // Method that will be called before first frame udpate and after Awake Method
    private void Start()
    {
        
    }

    // List of items
    public ItemDatabase itemDB;

    // Save function to XML
    public void SaveItems()
    {
        // Open a new xml file
        // Create xml serializer with the type to serialize
        XmlSerializer serializer = new XmlSerializer(typeof(ItemDatabase));
        // FileStream for the path and FileMode to access/modify the file
        FileStream stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/item_data.xml", FileMode.Create);
        // Serialize the item database with chosen stream
        serializer.Serialize(stream, itemDB);
        // Close the stream after use
        stream.Close();
    }

    // Load function
    public void LoadItems()
    {
        // Access an xml file
        // Create xml serializer with the type to serialize
        XmlSerializer serializer = new XmlSerializer(typeof(ItemDatabase));
        // FileStream for the path and FileMode to access/modify the file
        FileStream stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/item_data.xml", FileMode.Open);
        // De-Serialize the item database
        itemDB = serializer.Deserialize(stream) as ItemDatabase;
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

[System.Serializable]
// What's gonna be made into files
public class ItemDatabase
{
    // Allows you to modify the list name on xml file, not recommended to use spaces in between words
    [XmlArray("CombatEquipment")]
    // Create List with the type of Item Entry that will contain the items
    public List<ItemEntry> list = new List<ItemEntry>();
}

// Create enum for materials
public enum Material
{
    Wood,
    Copper,
    Iron,
    Steel,
    Gold
}