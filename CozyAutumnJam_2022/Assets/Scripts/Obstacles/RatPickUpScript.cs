using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatPickUpScript : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _ratCard;
    public void ActivateInteraction()
    {
        Debug.Log("Pickup");
        //add rat card to props deck
        CardManager.Instance.AddPropCard(_ratCard);
        gameObject.SetActive(false);
    }

    public void SetRat(GameObject card)
    {
        _ratCard = card;
    }
}
