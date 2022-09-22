using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateScript : MonoBehaviour
{
    [SerializeField] private bool isActivated;
    [SerializeField] private Sprite _upSprite;
    [SerializeField] private Sprite _downSprite;
    [SerializeField] private GameObject _vfx;
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
            
            //Play plate sfx
            gameObject.GetComponent<SpriteRenderer>().sprite = _downSprite;
            AkSoundEngine.PostEvent("Play_Plate", this.gameObject);
            isActivated = true;
            other.GetComponent<IPressurePalatable>().ActivatePlate();
        }
    }

    public void Deactivate()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = _upSprite;
        isActivated = false;
    }
}
