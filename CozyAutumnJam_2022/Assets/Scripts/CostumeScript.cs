using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostumeScript : MonoBehaviour, IInteractable
{
    //GOES ON COSTUMES GO's
    //QuestIndex Order:
    //StepIndex should always be 1
    [SerializeField] private CharacterWithProgression _character;
    [SerializeField] private bool _isSpiritCostume;

    public void ActivateInteraction()
    {
        //Backup check
        if (this.GetComponent<ItemTagScript>().IsCostume())
        {
            if(_isSpiritCostume)
            {
                QuestManager.Instance.CompleteQuestStepSpirit(_character);
            }
            else
            {
                QuestManager.Instance.CompleteQuestStepHuman(_character);
            }
        }
        //TODO:: needs to give the new costume card to the deck
    }

}
