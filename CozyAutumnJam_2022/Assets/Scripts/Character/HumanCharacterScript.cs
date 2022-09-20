using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HumanCharacterScript : MonoBehaviour, IInteractable
{
    //GOES ON HUMAN CHARACTER GO's
    [SerializeField] private CharacterWithProgression _characterSO;
    [SerializeField] private UnityEvent _characterEvent;
    private QuestManager.HumanQuest _characterQuest;
    
    private void Start() 
    {
        _characterQuest = QuestManager.Instance.HumanQuestList[_characterSO.QuestIndex];
    }
    public void ActivateInteraction()
    {
        //In the inspector, set to completequeststephuman from questmanager with _characterSO
        DialogueManager.Instance.StartStory(_characterSO.GetStory(), _characterEvent);
          
        //DialogueManager.TRANSLATED, _characterSO.IsTranslated,
    }
}
