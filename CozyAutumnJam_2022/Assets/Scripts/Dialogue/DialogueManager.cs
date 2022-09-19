using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using Ink.Runtime;
using TMPro;
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance
    {
        get{return instance;}
    }

    #region Story Bool Variables
    public const string ANIMATRONIC_ACTIVATED = "activated";
    public const string CHEST_OPEN = "open";
    public const string TRANSLATED = "has_translator";
    public const string GOT_COSTUME_REQUEST = "gotten_costume_request";
    public const string GAVE_COSTUME = "gave_costume";
    #endregion

    public void StartStoryViaButton(TextAsset inkFile)
    {
        StartStory(inkFile);
    }

    public void StartStory(TextAsset inkFile, UnityEvent endEvent = null)
    {
        StartStoryHelper(inkFile, endEvent);
    }

    public void StartStory(TextAsset inkFile, string varName, bool varValue, UnityEvent endEvent = null)
    {
        StartStoryHelper(inkFile, endEvent: endEvent, varName: varName, varValue: varValue);
    }

    public void StartStory(TextAsset inkFile, PasscodeChestScript chest, UnityEvent endEvent = null)
    {
        StartStoryHelper(inkFile, endEvent, chest);
    }

    public enum PuzzleStatus
    {
        WIP,
        Open,
        Close
    }
    public void ChoosePuzzleChoice(int index)
    {
        AkSoundEngine.PostEvent("Play_UISelect", this.gameObject);
        PuzzleStatus chestStatus = _chest.EnterCodeDigit(index);
        if (chestStatus != PuzzleStatus.WIP)
        {
            if(chestStatus == PuzzleStatus.Open)
            {
                SetStoryVariable(CHEST_OPEN, true);
            }

            _puzzleChoiceParent.SetActive(false);
            ChooseChoice(index);
        }
    }

    public void ChooseDialogueChoice(int index)
    {
        AkSoundEngine.PostEvent("Play_UISelect", this.gameObject);
        for(int i = 0; i < _story.currentChoices.Count; i++)
        {
            _dialogueChoices[i].gameObject.SetActive(false);
        }
        ChooseChoice(index);
    }
    public void OnAdvanceDialogue()
    {
        AdvanceDialogue();
        // TODO: Check if we're currently writing a sentence. If we are, load the full sentence.
    }

    private static DialogueManager instance;

    #region Story
    private static Story _story;
    [SerializeField] private UnityEvent _dialogueEndEvent;
    private string _currentSentence;
    private List <string> _tags;
    private IEnumerator _coroutine;
    #endregion

    #region Choices
    [SerializeField] private GameObject[] _dialogueChoices;
    [SerializeField] private GameObject _puzzleChoiceParent;

    [SerializeField] private GameObject[] _puzzleChoices;
    [SerializeField] private PasscodeChestScript _chest;
    #endregion
    	
    #region Dialogue Box
    [SerializeField] private GameObject _imageHolder;
    [SerializeField] private Image _charImage;
    [SerializeField] private GameObject _nameHolder;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _dialogueText;
    [SerializeField] private GameObject _canContinueIndicator;
    [SerializeField] private Animator animator;

    #endregion

    #region Checks
    [SerializeField] private bool _playerCanContinue;
    [SerializeField] private BoolVariable _playerInConvo;
    #endregion

    #region Character Info
    // TODO: Figure out how to do hat off boss
    private static string[] _emotions = new string[] {"Default", "Happy", "Tired"};
    [SerializeField] private string[] _characterNames;
    [SerializeField] private CharacterScriptableObject[] _characterInfos;
    #endregion

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void SetStoryVariable(string varName, bool varValue)
    {
        _story.variablesState[varName] = varValue;
    }

    private void StartStoryHelper(TextAsset inkFile, UnityEvent endEvent = null, PasscodeChestScript chest = null, string varName = "", bool varValue = false)
    {
        if(!_playerInConvo.Value)
        {
            _story = new Story(inkFile.text);
            _dialogueEndEvent = endEvent;
            _chest = chest;

            if(varName != "")
            {
                SetStoryVariable(varName, varValue);
            }
            
            animator.SetBool("isOpen", true);
            _playerInConvo.Value = true;
            
            StartTimer(WaitToStartStory());
        }
    }

    private void AdvanceDialogue()
    {
        if(_playerCanContinue && _playerInConvo && _story!=null)
        {
            AkSoundEngine.PostEvent("Play_CardFlip", this.gameObject);
            if (_story.canContinue)
            {
                _canContinueIndicator.SetActive(false);
                _playerCanContinue = false;

                _currentSentence = _story.Continue();
                
                ParseTags();

                if(_coroutine != null)
                {
                    StopCoroutine(_coroutine);
                }
                _coroutine = TypeSentence(_currentSentence);
                StartCoroutine(_coroutine);
                // Kristen TODO: Start character sfx                
            }
            else
            {
                FinishDialogue();
            }
        }
                
    }

    // Reads and separate the tags used in the ink file
    private void ParseTags()
    {
        _tags = _story.currentTags;
        string characterName = "";
        string emotion = "";

        foreach(string t in _tags)
        {
            string prefix = t.Split(' ')[0];
            string param = t.Substring(prefix.Length+1);
            
            
            switch(prefix.ToLower())
            {
                case "char":
                    characterName = param;
                    break;
                case "emotion":
                    emotion = param;
                    break;
                case "lines":
                    UpdateCurrentLines(Int32.Parse(param));
                    break;
                case "music":
                    // Kristen TODO: Play music
                    break;
            }
        }
        SetChar(characterName, emotion);

    }

    private void UpdateCurrentLines(int linesLeft)
    {
        for (int i = 0; i < linesLeft; i++)
        {
            _currentSentence += _story.Continue();
        }
    }

    private void SetChar(string charName, string emotion)
    {
        int characterIndex = Array.IndexOf(_characterNames, charName);
        if (characterIndex < 0)
        {
            _nameHolder.SetActive(false);
            _imageHolder.SetActive(false);
        }
        else
        {
            CharacterScriptableObject currentChar = _characterInfos[characterIndex];
            _nameText.text = currentChar.Name;
            
            int emotionIndex = emotion != "" ? Array.IndexOf(_emotions, emotion) : 0;
            _charImage = currentChar.GetEmote(emotionIndex);

            _nameHolder.SetActive(true);
            _imageHolder.SetActive(true);
        }
    }

    //Reads and displays the choices used in the ink file
    private void ParseChoices()
    {
        if(_story.currentChoices.Count == 10)
        {
            ActivateChoiceButton(_puzzleChoices);
            _puzzleChoiceParent.SetActive(true);
        }
        else if(_story.currentChoices.Count > 0)
        {
            ActivateChoiceButton(_dialogueChoices);
        }
        else
        {
            _canContinueIndicator.SetActive(true);
            _playerCanContinue = true;
        }
    }

    private void ActivateChoiceButton(GameObject[] choiceButton)
    {
        for(int i = 0; i <_story.currentChoices.Count; ++i)
        {
            Choice choice = _story.currentChoices[i];
            choiceButton[i].GetComponentInChildren<TextMeshProUGUI>().text = choice.text;
            choiceButton[i].SetActive(true);
        }
    }
    private void ChooseChoice(int index)
    {
        _story.ChooseChoiceIndex(index);
        _playerCanContinue = true;
        AdvanceDialogue();        
    }

    private IEnumerator TypeSentence(string sentence)
    {
        // TODO: There might be a small bug with \n character
        _dialogueText.text = "";

        for(int charIndex = 1; charIndex < sentence.Length; charIndex++)
        {
            _dialogueText.text = sentence.Substring(0,charIndex) 
                                + "<color=#ffffff00>" 
                                + sentence.Substring(charIndex)
                                + "</color>";
            yield return null;            
        }

        // Kristen TODO: end character SFX
        ParseChoices();
    }

    //Finishes the dialogue
    private void FinishDialogue()
    {
        Debug.Log("Finish Dialogue");
        animator.SetBool("isOpen", false);
        
        _nameText.text = "";
        _dialogueText.text = "";
        
        _playerInConvo.Value = false;
        _playerCanContinue = false;
        _story = null;

        StartTimer(TimeoutDialogue());
        TriggerEndBehavior();
    }

    private void StartTimer(IEnumerator timer)
    {
        StartCoroutine(timer);
    }

    private IEnumerator WaitToStartStory()
    {
        yield return new WaitForSeconds(0.25f);
        AdvanceDialogue();
    }

    private IEnumerator TimeoutDialogue()
    {
        yield return new WaitForSeconds(1f);
        _playerCanContinue = true;
    }
    private void TriggerEndBehavior()
    {
        if(_dialogueEndEvent != null)
        {
            _dialogueEndEvent.Invoke();
        }
    }
}