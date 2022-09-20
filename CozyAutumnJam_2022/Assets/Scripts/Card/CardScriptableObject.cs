using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardScriptableObject", menuName = "CozyAutumnJam_2022/CardScriptableObject", order = 0)]
public class CardScriptableObject : ScriptableObject 
{
#region SO Backing Fields
    [SerializeField] private string _abilityName;
    [SerializeField] private int _cardID;
    //maybe make it an Image
    [SerializeField] private Sprite _oracleCardSprite; 
    [SerializeField] private Sprite _oracleCardInfoSprite;
    [SerializeField] private string _abilityDescription;
#endregion

#region SO Getters
    public string AbilityName
    {
        get {return _abilityName;}
    }
    public int CardID
    {
        get {return _cardID;}
    }
    public Sprite OracleCardSprite
    {
        get {return _oracleCardSprite;}
    }
    public Sprite OracleCardInfoSprite
    {
        get {return _oracleCardInfoSprite;}
    }
    public string AbilityDescription
    {
        get {return _abilityDescription;}
    }
#endregion
}

