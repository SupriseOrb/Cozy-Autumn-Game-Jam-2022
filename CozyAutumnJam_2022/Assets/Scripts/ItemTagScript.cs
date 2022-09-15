using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTagScript : MonoBehaviour
{
    [SerializeField] private bool isTrash;
    [SerializeField] private bool isTrap;
    [SerializeField] private bool isTrappable;
    [SerializeField] private bool isRune;
    [SerializeField] private bool isPushable;
    [SerializeField] private bool isInteractable;
    [SerializeField] private bool isAnimatronic;
    [SerializeField] private bool isCharacter;

    public bool IsTrash()
    {
        return isTrash;
    }

    public bool IsTrap()
    {
        return isTrap;
    }

    public bool IsTrappable()
    {
        return isTrappable;
    }

    public bool IsRune()
    {
        return isRune;
    }

    public bool IsPushable()
    {
        return isPushable;
    }

    public bool IsInteractable()
    {
        return isInteractable;
    }

    public bool IsAnimatronic()
    {
        return isAnimatronic;
    }

    public bool IsCharacter()
    {
        return isCharacter;
    }
}
