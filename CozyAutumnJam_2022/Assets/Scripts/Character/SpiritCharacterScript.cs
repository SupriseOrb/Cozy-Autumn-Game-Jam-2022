using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpiritCharacterScript : MonoBehaviour, IInteractable
{
    //GOES ON SPIRIT CHARACTER GO's
    [SerializeField] private CharacterWithProgression _characterSO;
    [SerializeField] private UnityEvent _characterEvent;
    private QuestManager.SpiritQuest _characterQuest;

    private void Start() 
    {
        _characterQuest = QuestManager.Instance.SpiritQuestList[_characterSO.QuestIndex];
    }
    public void ActivateInteraction()
    {
        //In the inspector, set to completequeststepspirit from questmanager with _characterSO
        DialogueManager.Instance.StartStory(_characterSO.GetStory(), _characterEvent);
        
        //DialogueManager.TRANSLATED, _characterSO.IsTranslated,
    }
}
