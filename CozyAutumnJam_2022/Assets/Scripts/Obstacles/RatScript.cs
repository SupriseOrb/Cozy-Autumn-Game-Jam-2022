using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatScript : MonoBehaviour
{
    [SerializeField] private Transform[] _runToLocation;
    [SerializeField] private Transform _startLocation;
    private int currentTransformIndex;
    [SerializeField] private Transform currentLocation;
    [SerializeField] private float speed;
    [SerializeField] private Sprite _upSprite;
    [SerializeField] private Sprite _downSprite;
    [SerializeField] private Sprite _leftSprite;
    [SerializeField] private Sprite _rightSprite;


    public void Start()
    {
        currentLocation = _runToLocation[0];
        StartRunning();
    }

    public void StartRunning()
    {
        //Move to every location in _runToLocation in ORDER and also rotate towards the location
        //Note the locations will only be placed in 90 degree shit
        //rat runs directly to the location
        //When it runs to the final location it dissapears
        StartCoroutine(MoveObject(currentLocation));
    }
    IEnumerator MoveObject(Transform intendedTarget)
    {
        if (Mathf.Round(transform.position.y) > Mathf.Round(intendedTarget.position.y))
        {
            GetComponent<SpriteRenderer>().sprite = _downSprite;
        }
        else if (Mathf.Round(transform.position.y) < Mathf.Round(intendedTarget.position.y))
        {
            GetComponent<SpriteRenderer>().sprite = _upSprite;
        }
        else if (Mathf.Round(transform.position.x) > Mathf.Round(intendedTarget.position.x))
        {
            GetComponent<SpriteRenderer>().sprite = _leftSprite;
        }
        else if (Mathf.Round(transform.position.x) < Mathf.Round(intendedTarget.position.x))
        {
            GetComponent<SpriteRenderer>().sprite = _rightSprite;
        }
        
        while (Vector3.Distance(transform.position, currentLocation.position) > .001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentLocation.position, speed);
            yield return new WaitForSeconds(.01f);
        }

        currentTransformIndex++;
        if (currentTransformIndex < _runToLocation.Length)
        {
            currentLocation = _runToLocation[currentTransformIndex];
            StartCoroutine(MoveObject(currentLocation));
        }
        else if(currentTransformIndex == _runToLocation.Length)
        {
            currentTransformIndex = 0;
            gameObject.transform.position = _startLocation.position;
        }
        
        yield break;
    } 

    private void OnTriggerEnter2D(Collider2D other) 
    {

    }
}
