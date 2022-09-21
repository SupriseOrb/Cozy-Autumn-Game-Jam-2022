using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatScript : MonoBehaviour, IInteractable
{
    [SerializeField] private string _ratCard;
    public void ActivateInteraction()
    {
        if(CardManager.Instance.GetPropCard(_ratCard))
        {
            //Play cat meow
            AkSoundEngine.PostEvent("Play_CatMeow", this.gameObject);
            //Play poof vfx
            gameObject.SetActive(false);
        }
        else
        {
            //Play error sound or something
            AkSoundEngine.PostEvent("Play_Error", this.gameObject);
        }
    }
}
