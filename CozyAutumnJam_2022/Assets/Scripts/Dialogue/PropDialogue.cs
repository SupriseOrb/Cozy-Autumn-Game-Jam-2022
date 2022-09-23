using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropDialogue : MonoBehaviour, IProp
{
    [SerializeField] private TextAsset _story;

    public void ActivateProp()
    {
        if (this.TryGetComponent(out ItemTagScript tagScript) && tagScript.IsAnimatronic() && tagScript.IsPushable())
        {
            DialogueManager.Instance.StartStory(_story, DialogueManager.ANIMATRONIC_ACTIVATED, 
            this.gameObject.GetComponent<AnimatronicScript>().IsPowered);
        }
        else
        {
            DialogueManager.Instance.StartStory(_story);
        }
    }
}
