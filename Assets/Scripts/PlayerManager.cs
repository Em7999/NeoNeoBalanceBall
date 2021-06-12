using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    string PlayerName;
    public GameObject GetNameInput;
    public GameObject PresentName;
    public GameObject GetData;

    public void GetPlayerName()
    {
        PlayerName = GetNameInput.GetComponentInChildren<TMP_Text>().text;

    }

    public void GameEnd()
    {
        PresentName.GetComponent<TMP_Text>().text = PlayerName;
        GetData.GetComponent<GetData>().SinglePlayerInput(PlayerName, CreateLine._score);
    }
}
