using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatPickUpScript : MonoBehaviour, IInteractable
{
    public void ActivateInteraction()
    {
        Debug.Log("Pickup");
        //add rat card to props deck
    }
}
