using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableBoxScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private bool hasStartedMoving;
    // Start is called before the first frame update
    private void FixedUpdate() 
    {
        if(_rb.constraints != RigidbodyConstraints2D.FreezeAll && hasStartedMoving && _rb.velocity == Vector2.zero)
        {
            hasStartedMoving = false;
            _rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }    
        if(_rb.velocity != Vector2.zero)
        {
            hasStartedMoving = true;
        }
    }
    //TODO:: If I have time, create a trigger boundary to prevent boxes getting stuck on things
    public void StopMoving()
    {
        _rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
