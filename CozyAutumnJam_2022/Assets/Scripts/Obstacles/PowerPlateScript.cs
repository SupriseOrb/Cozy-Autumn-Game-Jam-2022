using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlateScript : MonoBehaviour
{
    [SerializeField] private GameObject _vfx;
    [SerializeField] private Sprite _upSprite;
    [SerializeField] private Sprite _downSprite;
private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.TryGetComponent(out AnimatronicScript boomBox))
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = _downSprite;
            //Play plate vfx
            Instantiate(_vfx, gameObject.transform.position, Quaternion.identity);            
            //Play plate sfx
            AkSoundEngine.PostEvent("Play_Plate", this.gameObject);
            AkSoundEngine.PostEvent("Play_PowerPlate", this.gameObject);
            other.GetComponent<AnimatronicScript>().SetPowerOfAnimatronic(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.TryGetComponent(out AnimatronicScript boomBox))
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = _upSprite;
            other.GetComponent<AnimatronicScript>().SetPowerOfAnimatronic(false);
        }
    }
}
