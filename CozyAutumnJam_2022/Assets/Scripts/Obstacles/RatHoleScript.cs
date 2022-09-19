using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatHoleScript : MonoBehaviour, IInteractable
{
    [SerializeField] private RatScript _rat;
    public void ActivateInteraction()
    {
        _rat.StartRunning();
    }
}
