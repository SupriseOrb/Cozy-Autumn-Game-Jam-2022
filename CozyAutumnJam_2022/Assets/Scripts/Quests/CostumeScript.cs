using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostumeScript : MonoBehaviour, IInteractable
{
    //GOES ON COSTUME GO's
    [SerializeField] private CharacterWithProgression _character;
    [SerializeField] private GameObject _costumeCard;
    [SerializeField] private bool _isSpiritCostume;
    [SerializeField] private GameObject _vfx;

    public void ActivateInteraction()
    {
        //Backup check
        if (this.GetComponent<ItemTagScript>().IsCostume())
        {
            //Costume VFX
            Instantiate(_vfx, gameObject.transform.position, Quaternion.identity);
            _character.IsCostumeCollected = true;
            CardManager.Instance.AddPropCard(_costumeCard);
            //Play Costume Get SFX
            AkSoundEngine.PostEvent("Play_CostumeGet", this.gameObject);
            if(!_isSpiritCostume)
            {
                //Check if costume was gotten w/o talking to spirit
                if (_character.StepIndex == 0)
                {
                    _character.AdvanceToStoryBeat(CharacterWithProgression.StoryBeat.GiveCostume);
                }
                else
                {
                    QuestManager.Instance.CompleteQuestStepHuman(_character);
                }
            }
            else
            {
                QuestManager.Instance.CompleteQuestStepSpirit(_character);
            }
            Destroy(gameObject);
        }
        //TODO:: needs to give the new costume card to the deck
    }

}
