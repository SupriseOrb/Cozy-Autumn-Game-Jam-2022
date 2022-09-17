using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostumeScript : MonoBehaviour, IInteractable
{
    //ID of the character this costume belongs to
    //Should be the same for spirit and human versions
    [SerializeField] private int _characterID;
    //the particular quest step this costume should complete
    //idk what specific # it should be just yet 
    //Step 3 for spirit costume? Step 5? for human costume
    [SerializeField] private int _questStep;

    public void ActivateInteraction()
    {
        //needs to give charID and qStep to dialogue manager
        QuestManager.Instance.CompleteQuestStep(_characterID, _questStep);
        //TODO:: needs to give the new costume card to the deck
    }

}
