using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritRatScript : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private SpriteRenderer ratSprite;
    [SerializeField] private Sprite _upSprite;
    [SerializeField] private Sprite _downSprite;
    [SerializeField] private Sprite _leftSprite;
    [SerializeField] private Sprite _rightSprite;
    [SerializeField] private GameObject _ratPickUp;
    [SerializeField] private Vector2 _runDirection;
    [SerializeField] private Rigidbody2D _ratRigidBody;
    [SerializeField] private Transform _lowerBound;
    [SerializeField] private Transform _upperBound;
    [SerializeField] private Vector2 _startLocation;

    public enum Direction
    {
        up = 0,
        right = 1,
        down = 2,
        left = 3
    }

#region Rat Obstacles
    [SerializeField] private Transform _stopLocation;
#endregion

    [SerializeField] private bool _isStopping = false;

    private void Start() 
    {
        _startLocation = transform.position;     
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.TryGetComponent(out RatBlockerScript blocker))
        {
            _isStopping = true;
            _stopLocation = other.GetComponent<RatBlockerScript>().GetStopLocation();
        }
        else if(other.TryGetComponent(out SpiderWebObject web) && web.CanTrap)
        {
            Instantiate(_ratPickUp, transform.position, other.transform.rotation, other.transform.parent);
            Destroy(gameObject);
        }
    }

    private void FixedUpdate() 
    {
        if(_isStopping)
        {
            _ratRigidBody.velocity = Vector2.zero;
            transform.position = Vector3.MoveTowards(transform.position, _stopLocation.position, speed);
        }
        if(transform.position.x > _upperBound.position.x || transform.position.x < _lowerBound.position.x || transform.position.y > _upperBound.position.y || transform.position.y < _lowerBound.position.y)
        {
            _ratRigidBody.velocity = Vector2.zero;
            transform.position = _startLocation;
            transform.position = Vector3.MoveTowards(transform.position, _startLocation, speed);
        }
    }

    public void StartRunning(Direction ratFacing)
    {
        _isStopping = false;
        if(ratFacing == Direction.up)
        {
            ratSprite.sprite = _upSprite;
            _runDirection = Vector2.up;
        }
        else if(ratFacing == Direction.right)
        {
            ratSprite.sprite = _rightSprite;
            _runDirection = Vector2.right;
        }
        else if(ratFacing == Direction.down)
        {
            ratSprite.sprite = _downSprite;
            _runDirection = Vector2.down;
        }
        else
        {
            ratSprite.sprite = _leftSprite;
            _runDirection = Vector2.left;
        }
        _ratRigidBody.velocity = _runDirection * speed;
    }
}
