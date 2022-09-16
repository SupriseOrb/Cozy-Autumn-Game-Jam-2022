using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatronicScript : MonoBehaviour, IAnimatronic
{
    [SerializeField] private string _musicName; 
    [SerializeField] private int _animatronicOrderIndex;
    [SerializeField] private bool _isPowered = true;
    [SerializeField] private AnimatronicManager _managerScript;

    public void ActivateAnimatronic()
    {
        if(_isPowered)
        {
            //Play music
            AkSoundEngine.PostEvent(_musicName, this.gameObject);
            _managerScript.checkSound(_animatronicOrderIndex);
        }
    }
}
