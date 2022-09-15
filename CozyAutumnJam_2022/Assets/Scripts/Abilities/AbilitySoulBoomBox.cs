using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySoulBoomBox : MonoBehaviour, IAbility
{
    [SerializeField] private CardScriptableObject _cardInformation;
    [SerializeField] private float _boomBoxRadius;
    private Vector3 _originalPos;
    private Vector3 _offset;

    public void ActivateAbility()
    {
        _originalPos = PlayerScript.Instance.transform.position;
        _offset = PlayerScript.Instance.transform.forward;
        Collider2D[] allColliders = Physics2D.OverlapCircleAll(_originalPos + _offset, _boomBoxRadius);
        int runeCount = 0;
        if (allColliders != null)
        {
            foreach (Collider2D c in allColliders)
            {
                if(c.TryGetComponent(out ItemTagScript interactable))
                {
                    //Only one character, set their bool variable to false
                    //c.gameObject.GetComponent<CharacterScriptableObject>().IsGibberish = false;
                    runeCount++;   
                }
                else {}
            }
        }
        if (runeCount == 0)
        {
            //No runes found
            Debug.Log("No runes found!");
        }
    }
}
