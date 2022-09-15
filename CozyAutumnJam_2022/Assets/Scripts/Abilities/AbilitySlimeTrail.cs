using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySlimeTrail : MonoBehaviour, IAbility
{
    [SerializeField] private CardScriptableObject _cardInformation;
    [SerializeField] private float _speed;
    private Vector3 _originalPos;
    private Vector3 _originalDir;
    private RaycastHit2D _rayCastHit;
    private GameObject _hitGO;

    public void ActivateAbility()
    {
        _originalPos = PlayerScript.Instance.transform.position;
        _originalDir = PlayerScript.Instance.transform.forward;
        _rayCastHit = Physics2D.Raycast(_originalPos, _originalDir);
        _hitGO = _rayCastHit.transform.gameObject;
        if (_hitGO.TryGetComponent(out ITaggable interactable))
        {
           _hitGO.GetComponent<Rigidbody2D>().AddForce(_originalDir * _speed);

           //If collides into something specific (probably with interface tag), do x?
           //If not, reset position?
        }
    }
}
