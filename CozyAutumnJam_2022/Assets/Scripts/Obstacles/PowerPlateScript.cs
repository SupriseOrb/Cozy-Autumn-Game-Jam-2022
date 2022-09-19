using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlateScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.TryGetComponent(out AnimatronicScript boomBox))
        {
            other.GetComponent<AnimatronicScript>().SetPowerOfAnimatronic(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.TryGetComponent(out AnimatronicScript boomBox))
        {
            other.GetComponent<AnimatronicScript>().SetPowerOfAnimatronic(false);
        }
    }
}
