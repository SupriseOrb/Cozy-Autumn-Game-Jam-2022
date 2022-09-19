using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderWebObject : MonoBehaviour
{
    //NOTE: Make sure the object holding this script is also holding the interface ITrap!
    [SerializeField] private bool _canTrap = true;
    public bool CanTrap {get {return _canTrap;}}

    // Update is called once per frame
    void FixedUpdate()
    {
        /*
        if (this.TryGetComponent(out ItemTagScript trap) && _canTrap)
        {
            Collider2D[] allColliders = Physics2D.OverlapCircleAll(this.transform.position, _spiderWebRadius);
            foreach (Collider2D c in allColliders)
            {
                if(c.TryGetComponent(out ItemTagScript trappable))
                {
                    //What are we doing to the rat? Play anim? destroy rat object?
                    //c.gameObject.SetActive(false);
                    //or
                    //Destroy(c.gameObject);

                    //Progress quest/story in some way  
                }
                else {}
            }
        }
        */
    }

    public void SetTrap(bool trapState)
    {
        _canTrap = trapState;
    }
}
