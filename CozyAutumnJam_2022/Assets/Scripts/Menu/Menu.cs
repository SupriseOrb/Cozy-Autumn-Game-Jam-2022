using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject _settingsCanvas;    

    protected void LoadScene(int sceneIndex)
    {
        //Play regular button sound
        AkSoundEngine.PostEvent("Play_UISelect", this.gameObject);
        SceneManager.LoadScene(sceneIndex);
    }

    protected void SetCanvasVisibility(Canvas canvas, bool canSee)
    {
        if (canSee)
        {
            //Play regular button sound
            AkSoundEngine.PostEvent("Play_UISelect", this.gameObject);
        }
        else
        {
            //Play back sound
            AkSoundEngine.PostEvent("Play_UIBack", this.gameObject);
        }
        
        canvas.enabled = canSee;
    }

    public void OpenSettings()
    {
        SetCanvasVisibility(_settingsCanvas.GetComponent<Canvas>(), true);
    }
    public void CloseSettings()
    {
        _settingsCanvas.GetComponent<SettingsManager>().CloseSettings();
        SetCanvasVisibility(_settingsCanvas.GetComponent<Canvas>(), false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
