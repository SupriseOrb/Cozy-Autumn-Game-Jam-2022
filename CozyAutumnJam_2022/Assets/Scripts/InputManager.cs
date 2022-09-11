using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private CardManager _cardManager;

    private InputAction _cycleLeftAction;
    private InputAction _cycleRightAction;
    private InputAction _viewInfoAction;

    void Awake()
    {
        _cycleLeftAction = _playerInput.actions["CycleLeft"];
        _cycleRightAction = _playerInput.actions["CycleRight"];
        _viewInfoAction = _playerInput.actions["ViewInfo"];
    }

    private void OnEnable() 
    {
        _cycleLeftAction.performed += OnCycleLeft;
        _cycleRightAction.performed += OnCycleRight;
        _viewInfoAction.performed += OnViewInfo;
    }

    public void OnCycleLeft(InputAction.CallbackContext context)
    {
        _cardManager.CycleCards(false);
        Debug.Log("Cycle Left!");
    }

    public void OnCycleRight(InputAction.CallbackContext context)
    {
        _cardManager.CycleCards(true);
        Debug.Log("Cycle Right!");
    }

    public void OnViewInfo(InputAction.CallbackContext context)
    {
        _cardManager.ViewCard();
        Debug.Log("View Info!");
        
    }
}
