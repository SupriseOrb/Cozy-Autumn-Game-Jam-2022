using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatBlockerScript : MonoBehaviour
{
    [SerializeField] private Transform _stopLocation;

    public Transform GetStopLocation()
    {
        return _stopLocation;
    }
}
