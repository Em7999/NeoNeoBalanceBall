using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeCount : MonoBehaviour
{
    private float i = 0;
    private float t = 0;
    private int minute;
    private int second;
    public GameObject gameTime;
    public GameObject ResultUI;
    public GameObject spawnThings;
    public GameObject uiManager;
    public GameObject[] smileFaces;
    private bool BigFoodYes;


    // Update is called once per frame
    void Update()
    {
        i += Time.deltaTime;
        minute = (int)i / 60;
        second = (int)i - 60 * ((int)i / 60);
        gameTime.GetComponent<TMP_Text>().text = string.Format("{0:D2}:{1:D2}", minute, second);
        if (i >= 360)
        {
            ShowResult();
            i = 0;
        }
       
        if ((int)i % 10 ==0 && BigFoodYes)
        {
            t = i;
            spawnThings.GetComponent<SpawnThings>().CreateBigFood();
            spawnThings.GetComponent<SpawnThings>().CreateBigBullet();
            BigFoodYes = false;
            Debug.Log(i);
        }
        if (i - t >= 5)
        {
            BigFoodYes = true;
        }
    }

    private void ShowResult()
    {
        uiManager.GetComponent<UIManager>().AfterGaming();
    }

}
