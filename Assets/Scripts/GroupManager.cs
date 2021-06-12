using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GroupManager : MonoBehaviour
{
    string GroupName;
    public GameObject GetNameInput;
    public GameObject PresentName;
    public GameObject GetData;
    public void GetGroupName()
    {
        GroupName = GetNameInput.GetComponentInChildren<TMP_Text>().text;
    }
    public void GameEnd()
    {
        PresentName.GetComponent<TMP_Text>().text = GroupName;
        GetData.GetComponent<GetData>().DualPlayerInput(GroupName, CreateLine._score, CreateLine.co_score);
    }
}
