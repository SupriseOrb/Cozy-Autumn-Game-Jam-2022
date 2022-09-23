using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTagScript : MonoBehaviour
{
    [SerializeField] private bool isTrash;
    [SerializeField] private bool isTrap; //
    [SerializeField] private bool isTrappable;
    [SerializeField] private bool isRune;
    [SerializeField] private bool isPushable; //
    [SerializeField] private bool isPressurePalatable;
    [SerializeField] private bool isAnimatronic;
    [SerializeField] private bool isCostume;

    public bool IsTrash()
    {
        return isTrash;
    }

    public bool IsTrap()
    {
        return isTrap;
    }

    public bool IsRune()
    {
        return isRune;
    }

    public bool IsPushable()
    {
        return isPushable;
    }

    public bool IsPressurePalatable()
    {
        return isPressurePalatable;
    }

    public bool IsAnimatronic()
    {
        return isAnimatronic;
    }

    public bool IsCostume()
    {
        return isCostume;
    }
}
