using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyCatScript : MonoBehaviour, IInteractable
{
    [SerializeField] private PushableBowlScript pushableBowl; 
    [SerializeField] private GameObject _vfx;
    public void ActivateInteraction()
    {
        if(pushableBowl.HitMaxCandies())
        {
            //Play cat meow
            AkSoundEngine.PostEvent("Play_CatMeow", this.gameObject);
            pushableBowl.ActivatePlate();
            //hide the cat?
            Instantiate(_vfx, gameObject.transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
}
