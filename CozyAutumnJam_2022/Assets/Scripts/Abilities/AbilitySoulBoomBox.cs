using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySoulBoomBox : MonoBehaviour, IAbility
{
    [SerializeField] private CardScriptableObject _cardInformation;
    [SerializeField] private float _boomBoxRadius;
    private Vector3 _originalPos;
    private Vector3 _offset;
    private bool _isAvailable = true;
    [SerializeField] private float _abilityCooldown = 1f;

    public void ActivateAbility()
    {
        if (_isAvailable == false)
        {
            Debug.Log("Boom Box Ability on cooldown!");
            return;
        }
        else
        {
            _originalPos = PlayerScript.Instance.transform.position;
            _offset = PlayerScript.Instance.getPlayerDirection();
            Collider2D[] allColliders = Physics2D.OverlapCircleAll(_originalPos + _offset, _boomBoxRadius);
            int animatronicCount = 0;
            if (allColliders.Length > 0)
            {
                foreach (Collider2D c in allColliders)
                {
                    if(c.TryGetComponent(out ItemTagScript interactable))
                    {
                        //Only one character, set their bool variable to false
                        if(c.gameObject.GetComponent<ItemTagScript>().IsAnimatronic())
                        {
                            c.GetComponent<AnimatronicScript>().ActivateAnimatronic();
                        }
                        animatronicCount++;   
                    }
                 else {}
                }
            }
            if (animatronicCount == 0)
            {
                //No animatronics found
                Debug.Log("No animatronics found!");
            }
            else
            {
                StartCoroutine(StartCooldown());
            }
        }
    }

    IEnumerator StartCooldown()
    {
        _isAvailable = false;
        yield return new WaitForSeconds (_abilityCooldown);
        _isAvailable = true;
    }
}
