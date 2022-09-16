using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : Menu
{
    [SerializeField] private Canvas _pauseMenuCanvas;
    [SerializeField] private BoolVariable _isPaused;  

    public void PauseGame()
    {
        if(!_isPaused.Value)
        {
            //Play pause sound
            AkSoundEngine.PostEvent("Play_UIPause", this.gameObject);
            _pauseMenuCanvas.enabled = true;
            _isPaused.Value = true;
        }
        
    }

    public void ResumeGame()
    {
        if(_isPaused.Value)
        {
            //Play resume sound
            AkSoundEngine.PostEvent("Play_UIResume", this.gameObject);
            _pauseMenuCanvas.enabled = false;
            _isPaused.Value = false;
        }    
    }

    public void MainMenu()
    {
        LoadScene(0);
    }
}
