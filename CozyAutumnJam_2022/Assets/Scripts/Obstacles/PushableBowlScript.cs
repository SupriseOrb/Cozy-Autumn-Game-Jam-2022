using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableBowlScript : MonoBehaviour, IPressurePalatable
{
    [SerializeField] private int maxCandies = 3;
    [SerializeField] private int currentCandies = 0;
    public void ActivatePlate()
    {
        if(currentCandies < maxCandies)
        {
            currentCandies++;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public bool HitMaxCandies()
    {
        return currentCandies >= maxCandies;
    }

    public void ResetCandies()
    {
        currentCandies = 0;
    }
}
