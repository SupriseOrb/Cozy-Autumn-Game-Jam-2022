using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTeleport : MonoBehaviour, IAbility
{
    [SerializeField] private CardScriptableObject _cardInformation;
    [SerializeField] private Vector3 _teleportDist;
    [SerializeField] private float _teleportRadius;
    private Vector3 _originalPos;

    public void ActivateAbility()
    {
        _originalPos = PlayerScript.Instance.transform.position;
        Collider2D[] allColliders = Physics2D.OverlapCircleAll(_originalPos + _teleportDist, _teleportRadius);
        if (allColliders != null)
        {
            PlayerScript.Instance.transform.position = _originalPos + _teleportDist;
            Debug.Log("You cannot teleport there!");
            StartCoroutine(TeleportBackDelay());
            PlayerScript.Instance.transform.position = _originalPos;
        }
        else
        {
           PlayerScript.Instance.transform.position = _originalPos + _teleportDist; 
        }
    }

    IEnumerator TeleportBackDelay()
    {
        yield return new WaitForSeconds(.5f);
    }
}
