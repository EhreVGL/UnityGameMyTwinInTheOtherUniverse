using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour
{
    private int currentScene;
    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        SaveLevel.singleton.SetLevel(currentScene + 1);
        Debug.Log(SaveLevel.singleton.GetLevel());
    }

    public void ReturnThisLevel()
    {
        SceneManager.LoadScene(currentScene);
    }

    public void LoadNewLevel()
    {
        if(currentScene == 4)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(currentScene + 1);
        }
    }
}
