using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatronicManager : MonoBehaviour, IAnimatronic
{
    [SerializeField] private string _musicName;
    [SerializeField] private bool[] _songsPlayedInOrder;
    [SerializeField] private bool _songComplete = false;
    // Start is called before the first frame update
    public void ActivateAnimatronic()
    {
        if(!_songComplete)
        {
            //Play music
            AkSoundEngine.PostEvent(_musicName, this.gameObject);
        }
        else
        {
            //Open Chest
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
