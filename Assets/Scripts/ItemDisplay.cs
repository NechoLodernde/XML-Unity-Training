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
        Display();
    }

    // Method to set individual item parent to panel
    public void Display()
    {
        foreach (ItemEntry item in XMLManager.XMLInstance.itemDB.list)
        {
            // Instantiate/Spawn item block to panel
            ItemBlock newItem = Instantiate(itemPrefab) as ItemBlock;
            // Set the item parent
            newItem.transform.SetParent(transform, false);
            newItem.Display(item);
        }
            
    }
}
