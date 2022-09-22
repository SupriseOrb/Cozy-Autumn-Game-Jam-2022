using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatronicScript : MonoBehaviour, IAnimatronic
{
    [SerializeField] private string _musicName; 
    [SerializeField] private int _animatronicOrderIndex;
    [SerializeField] private bool _isPowered = true;
    [SerializeField] private AnimatronicManager _managerScript;
    [SerializeField] private SpiritRatScript _ratScript = null;
    [SerializeField] private SpiritRatScript.Direction _runDirection;
    [SerializeField] private Sprite _unpoweredSprite;
    [SerializeField] private Sprite _poweredSprite;

    public void ActivateAnimatronic()
    {
        if(_isPowered)
        {
            //Play music
            SettingsManager.Instance.StartPuzzle(4f);
            AkSoundEngine.PostEvent(_musicName, this.gameObject);
            if(_managerScript != null)
            {
                _managerScript.checkSound(_animatronicOrderIndex);
            }
            else
            {
                Collider2D[] allColliders = Physics2D.OverlapCircleAll(gameObject.transform.position, 1);
                foreach(Collider2D c in allColliders)
                {
                    if(c.TryGetComponent(out SpiritRatScript rat))
                    {
                        _ratScript.StartRunning(_runDirection);
                    }
                }
            }
        }
    }

    public void SetPowerOfAnimatronic(bool power)
    {
        _isPowered = power;
        if(_isPowered)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = _poweredSprite;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = _unpoweredSprite;
        }
    }
}
