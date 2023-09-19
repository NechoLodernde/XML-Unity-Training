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
    }

    // Method that will be called before first frame udpate and after Awake Method
    private void Start()
    {
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

        XMLInstance = this;
    }

    // List of items
    public ItemDatabase itemDB;

    // Save function
    public void SaveItems()
    {
        // Open a new xml file
        // Create xml serializer with the type to serialize
        XmlSerializer serializer = new XmlSerializer(typeof(ItemDatabase));
        FileStream stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/item_data.xml", FileMode.Create);
        serializer.Serialize(stream, itemDB);
    }

    // Load function
    public void LoadItems()
    {

    }
}

// Allows class to be seen on Editor Inspector
[System.Serializable]
// Is what going to be populating the list
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