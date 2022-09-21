using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasscodeChestScript : MonoBehaviour, IInteractable
{
    [SerializeField] int[] _correctCode;
    [SerializeField] int _currentCodeIndex = 0;
    [SerializeField] bool _inputCorrectCode;
    [SerializeField] GameObject _costume;
    [SerializeField] TextAsset _chestDialogue;
    [SerializeField] private GameObject _vfx;

    public void ActivateInteraction()
    {
        _inputCorrectCode = true;
        _currentCodeIndex = 0;
        //Start chest dialogue
        DialogueManager.Instance.StartStory(_chestDialogue);
    }

    //Is called by the dialogue choice buttons
    public DialogueManager.PuzzleStatus EnterCodeDigit(int digit)
    {
        if(_correctCode[_currentCodeIndex] !=  digit)
        {
            _inputCorrectCode = false;
        }

        _currentCodeIndex++;

        if(_currentCodeIndex == _correctCode.Length)
        {
            if(_inputCorrectCode)
            {
                OpenChest();
                return DialogueManager.PuzzleStatus.Open;
            }
            else
            {
                return DialogueManager.PuzzleStatus.Close;
            }
        }
        else
        {
            return DialogueManager.PuzzleStatus.WIP;
        }
    }

    public void OpenChest()
    {
        Instantiate(_vfx);
        _costume.SetActive(true);
        gameObject.SetActive(false);
    }
}
