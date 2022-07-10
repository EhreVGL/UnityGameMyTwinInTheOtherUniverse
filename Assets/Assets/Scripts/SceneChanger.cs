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
    }

    public void ReturnThisLevel()
    {
        Debug.Log("ReturnThisLevel in SceneChanger");
        SceneManager.LoadScene(currentScene);
    }

    public void LoadNewLevel()
    {
        Debug.Log("LoadNewLevel in SceneChanger");
        SceneManager.LoadScene(currentScene + 1);
    }
}
