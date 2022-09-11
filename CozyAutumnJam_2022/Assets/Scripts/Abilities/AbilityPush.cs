using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPush : MonoBehaviour, IAbility
{
    [SerializeField] CardScriptableObject cardInformation;
    [SerializeField] GameObject pushProjectile;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateAbility()
    {
        Instantiate(pushProjectile);
    }
}
