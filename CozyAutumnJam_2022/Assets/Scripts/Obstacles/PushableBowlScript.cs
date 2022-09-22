using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableBowlScript : MonoBehaviour, IPressurePalatable
{
    [SerializeField] private int maxCandies = 3;
    [SerializeField] private int currentCandies = 0;
    [SerializeField] private GameObject[] groceries;
    public void ActivatePlate()
    {
        if(currentCandies < maxCandies)
        {
            groceries[currentCandies].SetActive(true);
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
