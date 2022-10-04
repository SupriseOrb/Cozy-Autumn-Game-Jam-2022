using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScript : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _teleportAnimationName;
    [SerializeField] private string _fadeAnimationName;
    [SerializeField] private DoubleVector2Var _dist;

    public void StartTeleportAnimation()
    {
        _animator.Play(_teleportAnimationName);
    }

    public void StartFadeAnimation()
    {
        _animator.Play(_fadeAnimationName);
    }

    public void TeleportAnimationEvent()
    {
        QuestManager.Instance.IntroTeleport(_dist);
    }

    public void FadeAnimationEvent()
    {
        QuestManager.Instance.IntroEnd();
    }
}
