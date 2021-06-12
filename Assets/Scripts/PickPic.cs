using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickPic : MonoBehaviour
{
    public GameObject[] PlayerOne;
    public GameObject[] PlayerTwo;

    public void PickPLayerOne(int i)
    {
        for(int t= 0; t<2; t++)
        {
            PlayerOne[t].SetActive(false);
        }
        PlayerOne[i].SetActive(true);
    }

    public void PickPLayerTwo(int i)
    {
        for (int t = 0; t < 2; t++)
        {
            PlayerTwo[t].SetActive(false);
        }
        PlayerTwo[i].SetActive(true);
    }
}
