using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OtherCharacterScript : MonoBehaviour, IInteractable
{
    //GOES ON HUMAN CHARACTER GO's
    [SerializeField] private CharacterWithProgression _characterSO;
    [SerializeField] private UnityEvent _characterEvent;
    
    public void ActivateInteraction()
    {
        DialogueManager.Instance.StartStory(_characterSO.GetStory(), _characterEvent);
          
        //DialogueManager.TRANSLATED, _characterSO.IsTranslated,
    }
}
