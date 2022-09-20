using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateScript : MonoBehaviour
{
    [SerializeField] private bool isActivated;
    private void OnTriggerStay2D(Collider2D other) 
    {
        Debug.Log("isActivated = " + isActivated);
        Debug.Log("Vel = " + other.GetComponent<Rigidbody2D>().velocity);
        if
        (
            !isActivated && 
            other.gameObject.TryGetComponent(out ItemTagScript tag) &&
            tag.IsPressurePalatable() && 
            other.GetComponent<Rigidbody2D>().velocity == Vector2.zero
        )
        {
            isActivated = true;
            other.GetComponent<IPressurePalatable>().ActivatePlate();
        }
    }

    public void Deactivate()
    {
        isActivated = false;
    }
}
