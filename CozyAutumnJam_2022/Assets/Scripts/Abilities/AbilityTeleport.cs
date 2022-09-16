using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTeleport : MonoBehaviour, IAbility
{
    [SerializeField] private CardScriptableObject _cardInformation;
    [SerializeField] private Vector3 _teleportDist;
    [SerializeField] private float _teleportRadius;
    private Vector3 _originalPos;
    private bool inHumanWorld = true;
    private bool _isAvailable = true;
    [SerializeField] private float teleportReturnDelay = .5f;
    [SerializeField] private float _abilityCooldown = 5f;

    public void ActivateAbility()
    {
        if (_isAvailable == false)
        {
            Debug.Log("Ability on cooldown!");
            return;
        }
        else
        {
            Debug.Log("teleporting...");
            _originalPos = PlayerScript.Instance.transform.position;
            Collider2D[] allColliders = Physics2D.OverlapCircleAll((inHumanWorld)? (_originalPos + _teleportDist) : (_originalPos - _teleportDist), _teleportRadius);
            if (allColliders.Length > 0)
            {
                PlayerScript.Instance.transform.position = ((inHumanWorld)? (_originalPos + _teleportDist) : (_originalPos - _teleportDist));
                Debug.Log("You cannot teleport there!");
                foreach(Collider2D obstacle in allColliders)
                {
                    Debug.Log("Hit: " + obstacle.gameObject.name);
                }
                StartCoroutine(TeleportBackDelay());
                StartCoroutine(StartCooldown());
            }
            else
            {
                PlayerScript.Instance.transform.position = (inHumanWorld)? (_originalPos + _teleportDist) : (_originalPos - _teleportDist); 
            }
            inHumanWorld = !inHumanWorld;
        }
    }

    IEnumerator TeleportBackDelay()
    {
        yield return new WaitForSeconds(teleportReturnDelay);
        PlayerScript.Instance.transform.position = _originalPos;
        inHumanWorld = !inHumanWorld;
    }

    IEnumerator StartCooldown()
    {
        _isAvailable = false;
        yield return new WaitForSeconds (_abilityCooldown);
        _isAvailable = true;
    }
}
