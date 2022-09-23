using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropDialogue : MonoBehaviour, IProp
{
    [SerializeField] private TextAsset _story;

    public void ActivateProp()
    {
        if (this.GetComponent<ItemTagScript>().IsAnimatronic())
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
