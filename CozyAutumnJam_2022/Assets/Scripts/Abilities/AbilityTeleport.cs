using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTeleport : MonoBehaviour, IAbility
{
    [SerializeField] private CardScriptableObject _cardInformation;
    [SerializeField] private Vector3 _teleportDist;
    [SerializeField] private float _teleportRadius;
    private Vector3 _originalPos;
    [SerializeField] private BoolVariable _inHumanWorld;
    private bool _isAvailable = true;
    [SerializeField] private float teleportReturnDelay = .5f;
    [SerializeField] private float _abilityCooldown = 5f;
    [SerializeField] private GameObject _vfx;
    [SerializeField] private bool abilityObtained = false;

    private void Start()
    {
        _inHumanWorld.Value = true;    
    }
    public void ActivateAbility()
    {
        if(abilityObtained)
        {
            if (_isAvailable == false)
            {
                Debug.Log("Ability on cooldown!");
                return;
            }
            else
            {
                Instantiate(_vfx, PlayerScript.Instance.transform);
                Debug.Log("teleporting...");
                _originalPos = PlayerScript.Instance.transform.position;
                Collider2D[] allColliders = Physics2D.OverlapCircleAll((_inHumanWorld.Value)? (_originalPos + _teleportDist) : (_originalPos - _teleportDist), _teleportRadius);
                if (allColliders.Length > 0)
                {
                    PlayerScript.Instance.transform.position = ((_inHumanWorld.Value)? (_originalPos + _teleportDist) : (_originalPos - _teleportDist));
                    Debug.Log("You cannot teleport there!");
                    foreach(Collider2D obstacle in allColliders)
                    {
                        Debug.Log("Hit: " + obstacle.gameObject.name);
                    }
                    StartCoroutine(TeleportBackDelay());
                    StartCoroutine(StartCooldown());
                    _inHumanWorld.Value = !_inHumanWorld.Value;
                }
                else
                {
                    //Teleport sound
                    AkSoundEngine.PostEvent("Play_Teleport", this.gameObject);
                    PlayerScript.Instance.transform.position = (_inHumanWorld.Value)? (_originalPos + _teleportDist) : (_originalPos - _teleportDist);
                    _inHumanWorld.Value = !_inHumanWorld.Value;
                    if(_inHumanWorld.Value)
                    {
                        AkSoundEngine.SetState("Gameplay", "HumanStore");
                    }
                    else
                    {
                        AkSoundEngine.SetState("Gameplay", "SpiritStore");
                    }
                }            
            }
        }
    }

    IEnumerator TeleportBackDelay()
    {
        yield return new WaitForSeconds(teleportReturnDelay);
        PlayerScript.Instance.transform.position = _originalPos;
        _inHumanWorld.Value = !_inHumanWorld.Value;
    }

    IEnumerator StartCooldown()
    {
        _isAvailable = false;
        yield return new WaitForSeconds (_abilityCooldown);
        _isAvailable = true;
    }

    public void ObtainAbility()
    {
        abilityObtained = true;
    }
}
