using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Vector2", fileName = "New Vector2 Var")]
public class Vector2Var : ScriptableObject
{
    [SerializeField] private Vector2 _defaultValue;
    [SerializeField] private Vector2 _currentValue;

    public Vector2 Value
    {
        get{return _currentValue;}
        set{_currentValue = value;}
    }

    void OnEnable()
    {
        _currentValue = _defaultValue;
    }
}
