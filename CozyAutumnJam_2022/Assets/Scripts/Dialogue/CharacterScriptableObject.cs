using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Character", menuName = "CozyAutumnJam_2022/Character/Character")]
public class CharacterScriptableObject : ScriptableObject
{   
    #region Fields
    [SerializeField] private string _name;
    // TODO: Figure out how to do it for V's Spirit who changes sprites MIDWAY
    [SerializeField] private Sprite[] _emotes;
    [SerializeField] private int _characterID;
    #endregion

    public string Name
    {
        get {return _name;}
    }

    public Sprite GetEmote(int index = 0)
    {
        return _emotes[index]; 
    }

    public int CharacterID
    {
        get {return _characterID;}
    }
}



