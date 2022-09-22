using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardArtImplementation : MonoBehaviour
{
    [SerializeField] private GameObject _smallVersion;
    [SerializeField] private GameObject _bigVersion;
    [SerializeField] private Sprite _icon;
    [SerializeField] private bool _isOracleCard;
    [SerializeField] private GameObject _hintHolder;
    [SerializeField] private GameObject[] _hiddenCardInfo;
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

    public void ShowHint()
    {
        if(_isOracleCard)
        {
            _hintHolder.SetActive(true);
        }
    }

    public void HideHint()
    {
        if(_isOracleCard)
        {
            _hintHolder.SetActive(false);
        }
    }

    public void ShowCardInfo()
    {
        for(int i = 0; i < _hiddenCardInfo.Length; i++)
        {
            _hiddenCardInfo[i].SetActive(true);
        }
    }
}
