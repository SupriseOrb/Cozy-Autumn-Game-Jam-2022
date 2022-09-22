using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatronicObstacleScript : MonoBehaviour
{
    [SerializeField] private string _musicName; 
    [SerializeField] private bool _isPowered = true;
    [SerializeField] private GameObject _vfx;

    public void ActivateAnimatronic()
    {
        if(_isPowered)
        {
            //Play music
            AkSoundEngine.PostEvent(_musicName, this.gameObject);
            //Add poof sfx
            Instantiate(_vfx, gameObject.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
