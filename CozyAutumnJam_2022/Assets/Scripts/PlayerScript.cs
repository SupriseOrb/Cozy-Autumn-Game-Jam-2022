using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    static private PlayerScript _instance;
    static public PlayerScript Instance { get { return _instance;}}

    //How far away the two shops are from each other in the game screen
    [SerializeField] private Vector3 teleportDistance;

    [Header("Movement Variables")]
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private Vector2 _playerDirection;

    void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        } 
    }

    private void FixedUpdate() 
    {
        _rb.MovePosition(_rb.position + _inputManager.MovementInput * _speed * Time.deltaTime);
        if(_inputManager.MovementInput != Vector2.zero)
        {
            _playerDirection = _inputManager.MovementInput;
        }
    }

    public Vector2 getPlayerDirection()
    {
        return _playerDirection;
    }
}
