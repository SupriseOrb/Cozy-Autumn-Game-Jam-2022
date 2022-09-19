using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterScript : MonoBehaviour, IInteractable
{
    [SerializeField] CharacterWithProgression _characterSO;
    [SerializeField] QuestManager.Quest _characterQuest;
    [SerializeField] UnityEvent _characterEvent;

    public void ActivateInteraction()
    {
        //In the inspector, set to completequeststep from questmanager with the questinfo from _characterQuest WTF IS THIS CODE
        DialogueManager.Instance.StartStory(_characterSO.GetStory(), _characterEvent);
    }

}
