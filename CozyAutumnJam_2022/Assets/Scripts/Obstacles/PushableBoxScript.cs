using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableBoxScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private bool hasStartedMoving;
    [SerializeField] private string _impactSound = "Play_BoxImpact";
    // Start is called before the first frame update
    private void FixedUpdate() 
    {
        if(_rb.constraints != RigidbodyConstraints2D.FreezeAll && hasStartedMoving && _rb.velocity == Vector2.zero)
        {
            //Play sound for when box hits wall
            AkSoundEngine.PostEvent(_impactSound, this.gameObject);
            hasStartedMoving = false;
            Reposition();
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

    public void Reposition()
    {
        float currentX = gameObject.transform.position.x;
        float currentY = gameObject.transform.position.y;
        currentX = Mathf.Round(currentX * 2) * .5f;
        currentY = Mathf.Round(currentY * 2) * .5f;
        gameObject.transform.position = new Vector2(currentX, currentY);
    }
}
