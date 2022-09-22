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
    [SerializeField] private string _interactString;
    [SerializeField] private string _resizeCardString;

    private InputAction _cycleLAction;
    private InputAction _cycleRAction;
    private InputAction _onSpaceAction;
    private InputAction _onEscapeAction;
    private InputAction _swapDAction;
    private InputAction _useDAction;
    private InputAction _movementAction;
    private InputAction _interactAction;
    private InputAction _resizeCardAction;
    

    void Awake()
    {
        _cycleLAction = _playerInput.actions[_cycleLString];
        _cycleRAction = _playerInput.actions[_cycleRString];
        _onSpaceAction = _playerInput.actions[_onSpaceString];
        _onEscapeAction = _playerInput.actions[_onEscapeString];
        _swapDAction = _playerInput.actions[_swapDString];
        _useDAction = _playerInput.actions[_useDString];
        _movementAction = _playerInput.actions[_movementString];
        _interactAction = _playerInput.actions[_interactString];
        _resizeCardAction = _playerInput.actions[_resizeCardString];
    }

    private void OnEnable() 
    {
        _cycleLAction.performed += OnCycleLeft;
        _cycleRAction.performed += OnCycleRight;
        _onSpaceAction.performed += OnSpace;
        _onEscapeAction.performed += OnEscape;
        _swapDAction.performed += OnSwapD;
        _useDAction.performed += OnUseD;
        _interactAction.performed += OnInteract;
        _resizeCardAction.performed += OnResizeCard;


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
        _interactAction.performed -= OnInteract;

        _movementAction.started -= OnMovement;
        _movementAction.performed -= OnMovement;
        _movementAction.canceled -= OnMovement; 
    }
    public void OnResizeCard(InputAction.CallbackContext context)
    {
        if (!_isPaused.Value && !_playerInConvo.Value)
        {
            _cardManager.EditCardSize();
        }
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
        
        if (_playerInConvo)
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

    public void OnInteract(InputAction.CallbackContext context)
    {
        Vector2 originalPos = PlayerScript.Instance.transform.position;
        Vector2 originalDir = PlayerScript.Instance.getPlayerDirection();
        float distance = 2f;
        RaycastHit2D _rayCastHit = Physics2D.Raycast(originalPos, originalDir, distance);
        Debug.DrawRay(originalPos, originalDir * distance, Color.red, 1);
        _movementInput = Vector2.zero;
        if(_rayCastHit.transform != null)
        {
            GameObject _hitGO = _rayCastHit.transform.gameObject;
            if (_hitGO.TryGetComponent(out IInteractable interactable))
            {
                interactable.ActivateInteraction();
            }
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
