using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySpiderWeb : MonoBehaviour, IAbility
{
    [SerializeField] CardScriptableObject _cardInformation;
    [SerializeField] GameObject _spiderWebGO;
    //Object with: sprite, ITrap, code to constantly check if it is colliding with any object with ITrappable (the rat),
    //and a bool canTrap (to check if it's in the correct state to catch the rat)
    private Vector3 _spiderWebPos;
    private Vector3 _offset;
    private Quaternion _spiderWebRot;

    public void ActivateAbility()
    {
        _spiderWebPos = PlayerScript.Instance.transform.position;
        _spiderWebRot = PlayerScript.Instance.transform.rotation;
        Instantiate(_spiderWebGO, _spiderWebPos + _offset, _spiderWebRot, PlayerScript.Instance.transform.root);
    }
}
