using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerBlock : MonoBehaviour
{
    public TMP_Text playerName, playerRole;

    public void Display(PlayerEntry player)
    {
        playerName.text = player.playerName;
        playerRole.text = player.playerRole.ToString();
    }
}
