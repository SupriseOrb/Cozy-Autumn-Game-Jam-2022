using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenu : Menu
{
    [SerializeField] private Canvas _pauseMenuCanvas;
    [SerializeField] private BoolVariable _isPaused;
    private EventSystem _currentEventSystem;
    [SerializeField] private GameObject _resumeButton;

    public void Start()
    {
        _currentEventSystem = EventSystem.current;
        _isPaused.Value = false;
        if(!_isMusicPlaying.Value)
        {
            AkSoundEngine.PostEvent("PlayMusicStart", this.gameObject);
            _isMusicPlaying.Value = true;
            Debug.Log("Playing Main Menu Music");
        }
        AkSoundEngine.SetState("Gameplay", "HumanStore");
    }
    public void PauseGame()
    {
        if(!_isPaused.Value)
        {
            //Play pause sound
            AkSoundEngine.PostEvent("Play_UIPause", this.gameObject);
            // TODO, drop the volume 50% of what the player has put
            _pauseMenuCanvas.enabled = true;
            _isPaused.Value = true;
            _currentEventSystem.SetSelectedGameObject(_resumeButton);
        }
        
    }

    public void ResumeGame()
    {
        if(_isPaused.Value)
        {
            //Play resume sound
            AkSoundEngine.PostEvent("Play_UIResume", this.gameObject);
            // TODO, resume the volume
            _pauseMenuCanvas.enabled = false;
            _isPaused.Value = false;
            _currentEventSystem.SetSelectedGameObject(null);
        }    
    }

    public void MainMenu()
    {
        LoadScene(0);
    }
}
