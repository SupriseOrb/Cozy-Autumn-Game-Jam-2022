using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlateScript : MonoBehaviour
{
    [SerializeField] private GameObject[] _resettableObjects;
    [SerializeField] private Vector2[] _resettablePositions;
    [SerializeField] private Sprite _upSprite;
    [SerializeField] private Sprite _downSprite;

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
            gameObject.GetComponent<SpriteRenderer>().sprite = _downSprite;
            ResetObjects();
        }
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.TryGetComponent(out PlayerScript player))
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = _upSprite;
        }
    }

    public void ResetObjects()
    {
        //Play plate sfx
        AkSoundEngine.PostEvent("Play_PlateReset", this.gameObject);
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
            if(_resettableObjects[i].TryGetComponent(out PushableBoxScript box))
            {
                box.StopMoving();
            }
            _resettableObjects[i].transform.position = _resettablePositions[i];
        }
    }
}
