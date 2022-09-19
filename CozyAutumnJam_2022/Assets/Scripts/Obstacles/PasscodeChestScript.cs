using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasscodeChestScript : MonoBehaviour, IInteractable
{
    [SerializeField] int[] _correctCode;
    [SerializeField] int _currentCodeIndex = 0;
    [SerializeField] GameObject _costume;
    [SerializeField] TextAsset _chestDialogue;

    public void ActivateInteraction()
    {
        _currentCodeIndex = 0;
        //Start chest dialogue
        DialogueManager.Instance.StartStory(_chestDialogue);
    }

    //Is called by the dialogue choice buttons
    public void EnterCodeDigit(int digit)
    {
        if(_correctCode[_currentCodeIndex] ==  digit)
        {
            _currentCodeIndex++;
            if(_currentCodeIndex == _correctCode.Length)
            {
                OpenChest();
            }
        }
        else
        {
            _currentCodeIndex = 0;
        }
    }

    public void OpenChest()
    {
        _costume.SetActive(true);
        gameObject.SetActive(false);
    }
}
