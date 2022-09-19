using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private CardManager _cardManager;
    [SerializeField] private PauseMenu _pauseMenu;
    [SerializeField] private BoolVariable _isPaused;
    [SerializeField] private BoolVariable _playerInConvo;

    [Header("OnMovement Vars")]
    [SerializeField] private Vector2 _movementInput;
    public Vector2 MovementInput
    {
        get{return _movementInput;}
    }

    [Header("Input Action Strings")]
    [SerializeField] private string _cycleLString;
    [SerializeField] private string _cycleRString;
    [SerializeField] private string _onSpaceString; 
    [SerializeField] private string _onEscapeString;
    [SerializeField] private string _swapDString;
    [SerializeField] private string _useDString;
    [SerializeField] private string _movementString;

    private InputAction _cycleLAction;
    private InputAction _cycleRAction;
    private InputAction _onSpaceAction;
    private InputAction _onEscapeAction;
    private InputAction _swapDAction;
    private InputAction _useDAction;
    private InputAction _movementAction;

    

    void Awake()
    {
        _cycleLAction = _playerInput.actions[_cycleLString];
        _cycleRAction = _playerInput.actions[_cycleRString];
        _onSpaceAction = _playerInput.actions[_onSpaceString];
        _onEscapeAction = _playerInput.actions[_onEscapeString];
        _swapDAction = _playerInput.actions[_swapDString];
        _useDAction = _playerInput.actions[_useDString];
        _movementAction = _playerInput.actions[_movementString];
    }

    private void OnEnable() 
    {
        _cycleLAction.performed += OnCycleLeft;
        _cycleRAction.performed += OnCycleRight;
        _onSpaceAction.performed += OnSpace;
        _onEscapeAction.performed += OnEscape;
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
        _onSpaceAction.performed -= OnSpace;
        _onEscapeAction.performed -= OnEscape;
        _swapDAction.performed -= OnSwapD;
        _useDAction.performed -= OnUseD;

        _movementAction.started -= OnMovement;
        _movementAction.performed -= OnMovement;
        _movementAction.canceled -= OnMovement; 
    }

    public void OnCycleLeft(InputAction.CallbackContext context)
    {
        if (!_isPaused.Value && !_playerInConvo.Value)
        {
            _cardManager.CycleCards(false);
        }
    }

    public void OnCycleRight(InputAction.CallbackContext context)
    {
        if (!_isPaused.Value && !_playerInConvo.Value)
        {
            _cardManager.CycleCards(true);
        }
    }

    public void OnSpace(InputAction.CallbackContext context)
    {
        if (!_isPaused.Value && !_playerInConvo.Value)
        {
            _cardManager.EditCardSize();
        }
        else if (_playerInConvo)
        {
            DialogueManager.Instance.OnAdvanceDialogue();
        }
    }

    public void OnEscape(InputAction.CallbackContext context)
    {
        if (!_isPaused.Value)
        {
            _pauseMenu.PauseGame();
        }
    }

    public void OnSwapD(InputAction.CallbackContext context)
    {
        if (!_isPaused.Value && !_playerInConvo.Value)
        {
            _cardManager.SwapDecks();
        }
    }

    public void OnUseD(InputAction.CallbackContext context)
    {
        if (!_isPaused.Value && !_playerInConvo.Value)
        {
            _cardManager.ActivateAbilityCard();
        }
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        if (!_isPaused.Value && !_playerInConvo.Value)
        {
            _movementInput = _movementAction.ReadValue<Vector2>();
        }
    }
}
