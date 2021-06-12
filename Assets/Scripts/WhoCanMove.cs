using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhoCanMove : MonoBehaviour
{
    public GameObject Player1;
    public GameObject Player2;
    public GameObject Port1;
    public GameObject Port2;
    public void PickP1()
    {
        Player1.GetComponent<Transform>().position = new Vector3(-4, 2, 0);
        Player2.GetComponent<PlayerController2>().enabled = false;
        Player2.GetComponent<BoxCollider2D>().enabled = false;
        Player2.GetComponent<Transform>().position = new Vector3(-8, -3, 0);
        Port2.SetActive(false);
    }

    public void PickP2()
    {
        Player2.GetComponent<Transform>().position = new Vector3(-4, 2, 0);
        Player1.GetComponent<PlayerController1>().enabled = false;
        Player1.GetComponent<BoxCollider2D>().enabled = false;
        Player1.GetComponent<Transform>().position = new Vector3(-8, -3, 0);
        Port1.SetActive(false);
    }
}
