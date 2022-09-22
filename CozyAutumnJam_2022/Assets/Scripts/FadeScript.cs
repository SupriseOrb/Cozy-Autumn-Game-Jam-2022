using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScript : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _fadeName;
    [SerializeField] private Transform _tpLocation;

    public void StartAnimation()
    {
        _animator.Play(_fadeName);
    }

    public void TeleportAnimationEvent()
    {
        
    }
}
