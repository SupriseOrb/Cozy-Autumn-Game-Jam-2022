using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : Menu
{
    [SerializeField] private Canvas _pauseMenuCanvas;
    
    void Start()
    {
        _isPaused.Value = false;
    }

    public void PauseGame()
    {
        if(!_isPaused)
        {
            _pauseMenuCanvas.enabled = true;
            _isPaused.Value = true;
        }
        
    }

    public void ResumeGame()
    {
        if(_isPaused)
        {
            _pauseMenuCanvas.enabled = false;
            _isPaused.Value = false;
        }    
    }

    public void MainMenu()
    {
        LoadScene(0);
    }
}
