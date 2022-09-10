using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardScriptableObject", menuName = "CozyAutumnJam_2022/CardScriptableObject", order = 0)]
public class CardScriptableObject : ScriptableObject 
{
#region SO Backing Fields
    //maybe make it an Image
    [SerializeField] private Sprite oracleCardSprite; 
#endregion
}
