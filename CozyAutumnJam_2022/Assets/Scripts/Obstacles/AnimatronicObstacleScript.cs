using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatronicObstacleScript : MonoBehaviour
{
    [SerializeField] private string _musicName; 
    [SerializeField] private bool _isPowered = true;

    public void ActivateAnimatronic()
    {
        if(_isPowered)
        {
            //Play music
            AkSoundEngine.PostEvent(_musicName, this.gameObject);
            //Add poof sfx
            Destroy(this.gameObject);
        }
    }
}
