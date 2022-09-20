using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlateScript : MonoBehaviour
{
    [SerializeField] private GameObject[] _resettableObjects;
    [SerializeField] private Vector2[] _resettablePositions;

    private void Start() 
    {
        _resettablePositions = new Vector2[_resettableObjects.Length];
        for(int i = 0; i < _resettableObjects.Length; i++)
        {
            _resettablePositions[i] = _resettableObjects[i].transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.TryGetComponent(out PlayerScript player))
        {
            ResetObjects();
        }
    }

    public void ResetObjects()
    {
        for(int i = 0; i < _resettableObjects.Length; i++)
        {
            _resettableObjects[i].SetActive(true);
            if(_resettableObjects[i].TryGetComponent(out PressurePlateScript plate))
            {
                plate.Deactivate();
            }
            if(_resettableObjects[i].TryGetComponent(out PushableBowlScript bowl))
            {
                bowl.ResetCandies();
            }
            if(_resettableObjects[i].TryGetComponent(out SpiritRatScript rat))
            {
                rat.ResetRat();
            }
            _resettableObjects[i].transform.position = _resettablePositions[i];
        }
    }
}
