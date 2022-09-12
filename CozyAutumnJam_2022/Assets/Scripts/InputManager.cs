using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private CardManager _cardManager;

    [Header("Movement Variables")]
    [SerializeField] private Vector2 _movementInput;
    [SerializeField] private float _speed;
    private Rigidbody2D _rb;

    [Header("Input Action Strings")]
    [SerializeField] private string _cycleLString;
    [SerializeField] private string _cycleRString;
    [SerializeField] private string _viewInfoString; 
    [SerializeField] private string _minimizeDString;
    [SerializeField] private string _swapDString;
    [SerializeField] private string _useDString;
    [SerializeField] private string _movementString;

    private InputAction _cycleLAction;
    private InputAction _cycleRAction;
    private InputAction _viewInfoDAction;
    private InputAction _minimizeDAction;
    private InputAction _swapDAction;
    private InputAction _useDAction;
    private InputAction _movementAction;

    void Awake()
    {
        _cycleLAction = _playerInput.actions[_cycleLString];
        _cycleRAction = _playerInput.actions[_cycleRString];
        _viewInfoDAction = _playerInput.actions[_viewInfoString];
        _minimizeDAction = _playerInput.actions[_minimizeDString];
        _swapDAction = _playerInput.actions[_swapDString];
        _useDAction = _playerInput.actions[_useDString];
        _movementAction = _playerInput.actions[_movementString];

        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() 
    {
        _cycleLAction.performed += OnCycleLeft;
        _cycleRAction.performed += OnCycleRight;
        _viewInfoDAction.performed += OnViewInfo;
        _minimizeDAction.performed += OnMinimizeD;
        _swapDAction.performed += OnSwapD;
        _useDAction.performed += OnUseD;

        _movementAction.started += OnMovement;
        _movementAction.performed += OnMovement;
        _movementAction.canceled += OnMovement;
    }

    private void OnDisable() 
    {
        _cycleLAction.performed -= OnCycleLeft;
        _cycleRAction.performed -= OnCycleRight;
        _viewInfoDAction.performed -= OnViewInfo;
        _minimizeDAction.performed -= OnMinimizeD;
        _swapDAction.performed -= OnSwapD;
        _useDAction.performed -= OnUseD;

        _movementAction.started -= OnMovement;
        _movementAction.performed -= OnMovement;
        _movementAction.canceled -= OnMovement; 

    }

    public void OnCycleLeft(InputAction.CallbackContext context)
    {
        _cardManager.CycleCards(false);
    }

    public void OnCycleRight(InputAction.CallbackContext context)
    {
        _cardManager.CycleCards(true);
    }

    public void OnViewInfo(InputAction.CallbackContext context)
    {
        _cardManager.ViewCard();
    }

    public void OnMinimizeD(InputAction.CallbackContext context)
    {
        _cardManager.CloseCard();
    }

    public void OnSwapD(InputAction.CallbackContext context)
    {
        _cardManager.SwapDecks();
    }

    public void OnUseD(InputAction.CallbackContext context)
    {
        _cardManager.ActivateAbilityCard();
    }

    private void FixedUpdate() 
    {
        _rb.MovePosition(_rb.position + _movementInput * _speed * Time.deltaTime);
    }
    public void OnMovement(InputAction.CallbackContext context)
    {
        _movementInput = _movementAction.ReadValue<Vector2>();
    }
}
