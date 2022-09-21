using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySoulBoomBox : MonoBehaviour, IAbility
{
    [SerializeField] private CardScriptableObject _cardInformation;
    [SerializeField] private float _boomBoxRadius;
    private Vector3 _originalPos;
    //private Vector3 _offset;
    private Vector3 _originalDir;
    [SerializeField] private float _distance = 2f;
    private RaycastHit2D _rayCastHit;
    private GameObject _hitGO;
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
            //_originalPos = PlayerScript.Instance.transform.position;
            //_offset = PlayerScript.Instance.getPlayerDirection();
            //Collider2D[] allColliders = Physics2D.OverlapCircleAll(_originalPos + _offset, _boomBoxRadius);
            //int animatronicCount = 0;
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
                    if(interactable.IsAnimatronic())
                    {
                        if(_hitGO.TryGetComponent(out AnimatronicScript animatronic))
                        {
                            animatronic.ActivateAnimatronic();
                        }
                        else if(_hitGO.TryGetComponent(out AnimatronicObstacleScript obstacle))
                        {
                            obstacle.ActivateAnimatronic();
                        }
                        else
                        {
                            _hitGO.GetComponent<AnimatronicManager>().ActivateAnimatronic();
                        }
                        StartCoroutine(StartCooldown());
                    }
                }
            }
            /*if (allColliders.Length > 0)
            {
                foreach (Collider2D c in allColliders)
                {
                    if(c.TryGetComponent(out ItemTagScript interactable))
                    {
                        //Only one character, set their bool variable to false
                        if(c.gameObject.GetComponent<ItemTagScript>().IsAnimatronic())
                        {
                            if(c.TryGetComponent(out AnimatronicScript animatronic))
                            {
                                c.GetComponent<AnimatronicScript>().ActivateAnimatronic();
                            }
                            else if(c.TryGetComponent(out AnimatronicObstacleScript obstacle))
                            {
                                obstacle.ActivateAnimatronic();
                            }
                            else
                            {
                                c.GetComponent<AnimatronicManager>().ActivateAnimatronic();
                            }
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
            }*/
        }
    }

    IEnumerator StartCooldown()
    {
        _isAvailable = false;
        yield return new WaitForSeconds (_abilityCooldown);
        _isAvailable = true;
    }
}
