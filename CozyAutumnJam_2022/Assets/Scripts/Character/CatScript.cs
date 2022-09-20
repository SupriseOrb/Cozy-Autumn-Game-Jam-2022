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
            //Play poof vfx
            gameObject.SetActive(false);
        }
        else
        {
            //error sound or something
        }
    }
}
