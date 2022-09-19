using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatScript : MonoBehaviour
{
    [SerializeField] private Transform[] _runToLocation;

    public void StartRunning()
    {
        //Move to every location in _runToLocation in ORDER and also rotate towards the location
        //Note the locations will only be placed in 90 degree shit
        //rat runs directly to the location
        //When it runs to the final location it dissapears
    } 
}
