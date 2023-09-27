using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisplay : MonoBehaviour
{
    // Referenced ItemBlock prefab to access in script
    public ItemBlock itemPrefab;
    // Start is called before the first frame update
    void Start()
    {
        XMLManager.XMLInstance.LoadItems();
        Display();
    }

    // Method to set individual item parent to panel
    public void Display()
    {
        // Iterate through each child in parent
        foreach (Transform child in transform)
        {
            // Destroy the child game object
            Destroy(child.gameObject);
        }

        // Iterate through every item on XML Manager item database list
        foreach (ItemEntry item in XMLManager.XMLInstance.itemDB.list)
        {
            // Instantiate/Spawn item block to panel
            ItemBlock newItem = Instantiate(itemPrefab) as ItemBlock;
            // Set the item parent
            newItem.transform.SetParent(transform, false);
            // Display the item on the panel
            newItem.Display(item);
        }
            
    }
}
