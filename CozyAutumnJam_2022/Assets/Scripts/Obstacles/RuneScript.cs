using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneScript : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _currentRune;
    [SerializeField] private Sprite _translatedRune;
    [SerializeField] private BoxCollider2D _runeHitbox;
    
    public void TranslateRune()
    {
        _currentRune.sprite = _translatedRune;
        _runeHitbox.enabled = false;
    }
}
