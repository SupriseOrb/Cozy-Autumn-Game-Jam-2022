using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatPickUpScript : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _ratCard;
    [SerializeField] private GameObject _vfx;
    public void ActivateInteraction()
    {
        Debug.Log("Pickup");
        //add rat card to props deck
        CardManager.Instance.AddPropCard(_ratCard);
        Instantiate(_vfx);
        gameObject.SetActive(false);
    }

    public void SetRat(GameObject card)
    {
        _ratCard = card;
    }
}
