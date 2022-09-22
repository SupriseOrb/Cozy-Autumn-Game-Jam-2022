using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variables/DoubleVector2", fileName = "New Double Vector2 Var")]
public class DoubleVector2Var : ScriptableObject
{
    [SerializeField] private Vector2 _defaultValueFirst;
    [SerializeField] private Vector2 _defaultValueSecond;
    [SerializeField] private Vector2 _currentValueFirst;
    [SerializeField] private Vector2 _currentValueSecond;

    public Vector2 ValueFirst
    {
        get{return _currentValueFirst;}
        set{_currentValueFirst = value;}
    }
    public Vector2 ValueSecond
    {
        get{return _currentValueSecond;}
        set{_currentValueSecond = value;}
    }

    void OnEnable()
    {
        _currentValueFirst = _defaultValueFirst;
        _currentValueSecond = _defaultValueSecond;
    }
}
