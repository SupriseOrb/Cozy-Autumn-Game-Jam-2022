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
}
