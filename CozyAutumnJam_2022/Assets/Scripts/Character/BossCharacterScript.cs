using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossCharacterScript : MonoBehaviour, IInteractable
{
    //GOES ON HUMAN CHARACTER GO's
    [SerializeField] private CharacterWithProgression _bossSO;
    [SerializeField] private UnityEvent _characterEvent;
    private QuestManager.BossQuest _bossQuest;
    
    private void Start() 
    {
        _bossQuest = QuestManager.Instance.BossQuestList[_bossSO.QuestIndex];
    }
    public void ActivateInteraction()
    {
        //In the inspector, set to completequeststephuman from questmanager with _characterSO
        DialogueManager.Instance.StartStory(_bossSO.GetStory(), _characterEvent);
        
    }
}
