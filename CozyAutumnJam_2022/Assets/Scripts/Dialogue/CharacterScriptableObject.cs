using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Character", menuName = "CozyAutumnJam_2022/Character")]
public class CharacterScriptableObject : ScriptableObject
{   
    #region Fields
    [SerializeField] private string _name;
    [SerializeField] private Image[] _emotes;
    [SerializeField] private bool _isTranslatedDefault;
    [SerializeField] private bool _isTranslated;

    [SerializeField] private TextAsset[] _story;   
    
    #endregion

    public string Name
    {
        get {return _name;}
    }

    public Image GetEmote(int index = 0)
    {
        return _emotes[index]; 
    }

    public bool IsTranslated
    {
        get {return _isTranslated;}
        set {_isTranslated = value;}
    }

    private void OnEnable()
    {
        _isTranslated = _isTranslatedDefault;
    }
}



