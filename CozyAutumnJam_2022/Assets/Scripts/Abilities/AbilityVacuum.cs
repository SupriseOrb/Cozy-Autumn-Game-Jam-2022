using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityVacuum : MonoBehaviour, IAbility
{
    [SerializeField] private CardScriptableObject _cardInformation;
    [SerializeField] private float _vacuumRadius;
    private Vector3 _originalPos;
    private Vector3 _offset;


    public void ActivateAbility()
    {
        _originalPos = PlayerScript.Instance.transform.position;
        _offset = PlayerScript.Instance.transform.forward;
        Collider2D[] allColliders = Physics2D.OverlapCircleAll(_originalPos, _vacuumRadius);
        if (allColliders != null)
        {
            foreach (Collider2D c in allColliders)
            {
                if (c.TryGetComponent(out ITrash trash))
                {
                    //Disable or Destroy them (any animations?):
                    //Destroy(c.gameObject);
                    //or
                    //c.gameObject.SetActive(false);
                }
                else {}
            }
        }
    }

}
