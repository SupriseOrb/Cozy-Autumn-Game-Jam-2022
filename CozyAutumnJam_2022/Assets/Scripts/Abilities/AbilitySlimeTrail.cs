using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySlimeTrail : MonoBehaviour, IAbility
{
    [SerializeField] private CardScriptableObject _cardInformation;
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _originalPos;
    [SerializeField] private Vector3 _originalDir;
    [SerializeField] private float _distance = 2f;
    private RaycastHit2D _rayCastHit;
    private GameObject _hitGO;
    private bool _isAvailable = true;
    [SerializeField] private float _abilityCooldown = 3f;

    public void ActivateAbility()
    {
        if (_isAvailable == false)
        {
            Debug.Log("Slime Trail Ability on cooldown!");
            return;
        }
        else
        {
            _originalPos = PlayerScript.Instance.transform.position;
            _originalDir = PlayerScript.Instance.getPlayerDirection();
            _rayCastHit = Physics2D.Raycast(_originalPos, _originalDir, _distance);
            Debug.DrawRay(_originalPos, _originalDir * _distance, Color.red, 1);
            if(_rayCastHit.transform != null)
            {
                _hitGO = _rayCastHit.transform.gameObject;
                if (_hitGO.TryGetComponent(out ItemTagScript interactable))
                {
                    Debug.Log("hit something");
                    if(_hitGO.GetComponent<ItemTagScript>().IsPushable())
                    {
                        // Kristen Todo: Slime push sfx
                        Debug.Log("Hit a pushabLe");
                        _hitGO.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                        _hitGO.GetComponent<Rigidbody2D>().AddForce(_originalDir * _speed, ForceMode2D.Impulse);
                        StartCoroutine(StartCooldown());
                    }
                    //If collides into something specific (probably with interface tag), do x?
                    //If not, reset position?
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
