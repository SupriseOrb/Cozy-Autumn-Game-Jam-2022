using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySlimeTrail : MonoBehaviour, IAbility
{
    [SerializeField] private CardScriptableObject _cardInformation;
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _originalPos;
    [SerializeField] private Vector3 _originalDir;
    [SerializeField] private float _distance = 200;
    private RaycastHit2D _rayCastHit;
    private GameObject _hitGO;

    public void ActivateAbility()
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
                Debug.Log("hitsomething");
                if(_hitGO.GetComponent<ItemTagScript>().IsPushable())
                {
                    Debug.Log("Hit a pushabke");
                    _hitGO.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                    _hitGO.GetComponent<Rigidbody2D>().AddForce(_originalDir * _speed);
                }
            //If collides into something specific (probably with interface tag), do x?
            //If not, reset position?
            }
        }
    }
}
