using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{

    public void StartBtn()
    {
        SceneManager.LoadSceneAsync("ForestScene");
    }

    public void ContinueBtn()
    {

    }

    public void ExitBtn()
    {
        Application.Quit();
    }
}
