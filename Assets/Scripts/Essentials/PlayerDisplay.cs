using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDisplay : MonoBehaviour
{
    public PlayerBlock playerInfoPrefab;

    public void Display()
    {
        PlayerXMLManager.PlayerXMLInstance.LoadPlayer();
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        foreach(PlayerEntry player in PlayerXMLManager.PlayerXMLInstance.playerDB.list)
        {
            PlayerBlock newPlayer = Instantiate(playerInfoPrefab);
            newPlayer.transform.SetParent(transform, false);
            newPlayer.Display(player);
        }
    }
}
