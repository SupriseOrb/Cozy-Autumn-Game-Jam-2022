using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    static private PlayerScript _instance;
    static public PlayerScript Instance { get { return _instance;}}

    //How far away the two shops are from each other in the game screen
    //Currently unused
    [SerializeField, Tooltip("Currently Unused")] private Vector3 _teleportDistance;

    [Header("Movement Variables")]
    [SerializeField, Tooltip("The speed of the player.")] 
    private float _speed;
    [SerializeField, Tooltip("How quickly the player will reach the target speed.")] 
    private float _speedSmoothTime;
    //Holder for current position (for smoothing movement)
    private Vector2 _currentInputVector;
    //Holder for current velocity (for smoothing movement)
    private Vector2 _currentVelocity;
    private Vector2 _moveInput;

    [Header("References")]
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private Vector2 _playerDirection;

    void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        } 
    }

    private void FixedUpdate() 
    {
        _moveInput = _inputManager.MovementInput;
        _currentInputVector = Vector2.SmoothDamp(_currentInputVector, _inputManager.MovementInput, ref _currentVelocity, _speedSmoothTime);
        _rb.MovePosition(_rb.position + _currentInputVector * _speed * Time.deltaTime);
        if(_moveInput != Vector2.zero)
        {
            _playerDirection = _inputManager.MovementInput;
        }
    }

    public Vector2 getPlayerDirection()
    {
        return _playerDirection;
    }
}
