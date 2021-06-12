using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
   public void ToScene1()
    {
        SceneManager.LoadSceneAsync("OnePlayer");
    }

    public void ToScene2()
    {
        SceneManager.LoadSceneAsync("TwoPlayer");
    }
}
