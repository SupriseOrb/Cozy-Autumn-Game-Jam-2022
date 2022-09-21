using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityVacuum : MonoBehaviour, IAbility
{
    [SerializeField] private CardScriptableObject _cardInformation;
    [SerializeField] private float _vacuumRadius;
    private Vector3 _originalPos;
    private Vector3 _offset;
    private bool _isAvailable = true;
    [SerializeField] private float _abilityCooldown = 1f;


    public void ActivateAbility()
    {
        if (_isAvailable == false)
        {
            Debug.Log("Vacuum Ability on cooldown!");
            return;        
        }
        else
        {
            _originalPos = PlayerScript.Instance.transform.position;
            _offset = PlayerScript.Instance.getPlayerDirection();
            Collider2D[] allColliders = Physics2D.OverlapCircleAll(_originalPos, _vacuumRadius);
            int trashCount = 0;
            if (allColliders.Length > 0)
            {
                foreach (Collider2D c in allColliders)
                {
                    if (c.TryGetComponent(out ItemTagScript trash))
                    {
                        if(c.GetComponent<ItemTagScript>().IsTrash())
                        {
                            c.gameObject.SetActive(false);
                            trashCount++;
                        }
                    
                    }
                    else {}
                }
                if (trashCount == 0)
                {
                    Debug.Log("No trash found!");
                }
                else
                {
                    //Vacuum suck sfx
                    AkSoundEngine.PostEvent("Play_Vaccuum", this.gameObject);
                    StartCoroutine(StartCooldown());
                }
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
