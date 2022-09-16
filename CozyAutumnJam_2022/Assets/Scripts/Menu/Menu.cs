using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject _settingsCanvas;    

    protected void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    protected void SetCanvasVisibility(Canvas canvas, bool canSee)
    {
        if (canSee)
        {
            // Kristen TODO: Play regular button sound
        }
        else
        {
            // Kristen TODO: Play back sound
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
