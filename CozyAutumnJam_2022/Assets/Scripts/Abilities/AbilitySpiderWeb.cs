using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySpiderWeb : MonoBehaviour, IAbility
{
    [SerializeField] CardScriptableObject _cardInformation;
    [SerializeField] GameObject _spiderWebGO;
    //Object with: sprite, ITrap, code to constantly check if it is colliding with any object with ITrappable (the rat),
    //and a bool canTrap (to check if it's in the correct state to catch the rat)
    [SerializeField] GameObject _lastWeb;
    [SerializeField] float _spiderWebDetectRadius;
    private Quaternion _spiderWebRot;
    [SerializeField] private bool abilityObtained = false;

    public void ActivateAbility()
    {
        if(abilityObtained)
        {
            _spiderWebRot = PlayerScript.Instance.transform.rotation;
            Collider2D[] allColliders = Physics2D.OverlapCircleAll(PlayerScript.Instance.transform.position, _spiderWebDetectRadius);
            if(allColliders.Length > 0)
            {
                foreach (Collider2D c in allColliders)
                {
                    if(c.TryGetComponent(out ItemTagScript trappable) && c.GetComponent<ItemTagScript>().IsTrap())
                    {
                        if(_lastWeb != null)
                        {
                            Destroy(_lastWeb);
                        }
                        _lastWeb = Instantiate(_spiderWebGO, c.transform.position, _spiderWebRot, c.transform);
                        //Spider web place down sfx
                        AkSoundEngine.PostEvent("Play_SpiderWeb", this.gameObject);
                    }
                }
            }
        }
    }

    public void ObtainAbility()
    {
        abilityObtained = true;
    }
}
