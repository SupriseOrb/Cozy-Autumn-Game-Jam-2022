using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardArtImplementation : MonoBehaviour
{
    [SerializeField] private GameObject _smallVersion;
    [SerializeField] private GameObject _bigVersion;
    [SerializeField] private Sprite _icon;
    public Sprite Icon
    {
        get{return _icon;}
    }

    public void Expand()
    {
        _smallVersion.SetActive(false);
        _bigVersion.SetActive(true);
    }

    public void Minimize()
    {
        _smallVersion.SetActive(true);
        _bigVersion.SetActive(false);
    }
}
