using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    
    public bool _isMenuScene;

    public GameObject settingsWindow;
    public void StartGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    void Update() 
    {
        if(!_isMenuScene)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                ActivateSettingsWindow();
            }
        }
    }

    public void ActivateSettingsWindow()
    {
        settingsWindow.SetActive(true);
    }

    public void Exit() 
    {
        Application.Quit();
    }
    
}


