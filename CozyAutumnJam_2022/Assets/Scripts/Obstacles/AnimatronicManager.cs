using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatronicManager : MonoBehaviour, IAnimatronic, IInteractable
{
    [SerializeField] private string _musicName;
    [SerializeField] private bool[] _songsPlayedInOrder;
    [SerializeField] private bool _songComplete = false;
    [SerializeField] GameObject _costume;
    [SerializeField] private GameObject _vfx;

    // Start is called before the first frame update
    public void ActivateAnimatronic()
    {
        ActivateInteraction();
    }

    public void ActivateInteraction()
    {
        if(!_songComplete)
        {
            //Play music
            AkSoundEngine.PostEvent(_musicName, this.gameObject);
            SettingsManager.Instance.StartPuzzle(10f);
        }
        else
        {
            //Open Chest
            Instantiate(_vfx, gameObject.transform.position, Quaternion.identity);
            _costume.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public void checkSound(int index)
    {
        for(int i = 0; i < index; i++)
        {
            if(!_songsPlayedInOrder[i])
            {
                ResetSongs();
                return;
            }
        }
        _songsPlayedInOrder[index] = true;
        if(index == _songsPlayedInOrder.Length - 1)
        {
            _songComplete = true;
            //Open Chest
            Instantiate(_vfx, gameObject.transform.position, Quaternion.identity);
            _costume.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public void ResetSongs()
    {
        for(int i = 0; i < _songsPlayedInOrder.Length; i++)
        {
            _songsPlayedInOrder[i] = false;
        }
    }
}
