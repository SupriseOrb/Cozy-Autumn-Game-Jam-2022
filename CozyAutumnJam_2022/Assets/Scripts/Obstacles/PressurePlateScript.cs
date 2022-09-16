using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateScript : MonoBehaviour
{
    [SerializeField] private bool isActivated;
    private void OnTriggerStay2D(Collider2D other) 
    {
        if
        (
            !isActivated && 
            other.gameObject.TryGetComponent(out ItemTagScript tag) &&
            other.GetComponent<ItemTagScript>().IsPressurePalatable() && 
            other.GetComponent<Rigidbody2D>().velocity == Vector2.zero
        )
        {
            isActivated = true;
            other.GetComponent<IPressurePalatable>().ActivatePlate();
        }
    }
}
