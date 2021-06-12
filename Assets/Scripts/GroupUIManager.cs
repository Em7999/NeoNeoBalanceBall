using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GroupUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject _whichDual;
    public GameObject _PickPic;
    public GameObject _duringGaming;
    public GameObject _demonstration;
    public GameObject _result;
    public GameObject _getName;
    public GameObject _wall;
    public Transform _player1;
    public Transform _player2;
    public Transform _bg;
    public GameObject _playerManager;

    public void AfterDualMode()
    {
        _whichDual.SetActive(false);
        _PickPic.SetActive(true);
    }
    public void AfterPickPic()
    {
        _PickPic.SetActive(false);
        _getName.SetActive(true);

    }
    public void AfterName()
    {
        _getName.SetActive(false);
        _demonstration.SetActive(true);
    }

    public void AfterDemonstration()
    {
        _demonstration.SetActive(false);
        _duringGaming.SetActive(true);
        _wall.SetActive(true);
        DestroyAllClones();
        CreateLine._score = 0;
        CreateLine.co_score = 0;
        _player1.position = new Vector3(-4, 2, 0);
        _player2.position = new Vector3(-4, -2, 0);
        _bg.position = new Vector3(143.6f, 0, 0);

    }

    public void AfterGaming()
    {
        _duringGaming.SetActive(false);
        _result.SetActive(true);
        _wall.SetActive(false);
        DestroyAllClones();
        _playerManager.GetComponent<GroupManager>().GameEnd();
    }

    public void AfterResult()
    {

        SceneManager.LoadSceneAsync("ModeScene");


    }

    public void DestroyAllClones()
    {
        GameObject[] AllBullets = GameObject.FindGameObjectsWithTag("Bullet");
        GameObject[] AllFood = GameObject.FindGameObjectsWithTag("Food");
        GameObject[] AllBigBullets = GameObject.FindGameObjectsWithTag("BigBullet");
        GameObject[] AllBigFood = GameObject.FindGameObjectsWithTag("BigFood");
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Bullet").Length; i++)
        {
            Destroy(AllBullets[i]);
        }
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Food").Length; i++)
        {
            Destroy(AllFood[i]);
        }
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("BigBullet").Length; i++)
        {
            Destroy(AllBigBullets[i]);
        }
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("BigFood").Length; i++)
        {
            Destroy(AllBigFood[i]);
        }
    }
}