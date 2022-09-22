using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatHoleScript : MonoBehaviour, IInteractable
{
    [SerializeField] private RatScript _rat;
    [SerializeField] private SpiritRatScript _spiritRat;
    public void ActivateInteraction()
    {
        if(_rat != null)
        {
            _rat.StartRunning();
        }
        else if(_spiritRat != null)
        {
            _spiritRat.ResetRat();
            _spiritRat.StartRunning(SpiritRatScript.Direction.left);
        }
    }
}
