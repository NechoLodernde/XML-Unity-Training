using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemBlock : MonoBehaviour
{
    // Text field for item block
    public TMP_Text itemName, itemMaterial, itemValue;

    public void Display(ItemEntry item)
    {
        itemName.text = item.itemName;
        itemMaterial.text = item.itemMaterial.ToString();
        itemValue.text = "$" + item.itemValue.ToString();
    }
}
