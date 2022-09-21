using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyCatScript : MonoBehaviour, IInteractable
{
    [SerializeField] private PushableBowlScript pushableBowl; 
    public void ActivateInteraction()
    {
        if(pushableBowl.HitMaxCandies())
        {
            //Play cat meow
            AkSoundEngine.PostEvent("Play_CatMeow", this.gameObject);
            pushableBowl.ActivatePlate();
            //hide the cat?
            gameObject.SetActive(false);
        }
    }
}
