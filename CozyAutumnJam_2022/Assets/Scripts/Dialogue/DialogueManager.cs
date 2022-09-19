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

    public const string ANIMATRONIC_ACTIVATED = "activated";
    public const string CHEST_OPEN = "open";
    public const string TRANSLATED = "has_translator";
    public const string GOT_COSTUME_REQUEST = "gotten_costume_request";
    public const string GAVE_COSTUME = "gave_costume";

    /*Starts the dialogue
        inkFile : the dialogue that will start
        endEvent : any behavior that occurs at the end of the dialogue; set to null by default
    */
    public void StartStoryViaButton(TextAsset inkFile)
    {
        StartStory(inkFile);
    }
    
    public void StartStory(TextAsset inkFile, UnityEvent endEvent = null, PasscodeChestScript chest = null, string varName = "", bool varValue = false)
    {
        if(!_playerInConvo.Value)
        {
            _story = new Story(inkFile.text);
            _dialogueEndEvent = endEvent;
            _chest = chest;
            if(varName != "")
            {
                SetVariable(varName, varValue);
            }
            
            animator.SetBool("isOpen", true);
            _playerInConvo.Value = true;
            
            StartTimer(WaitToStartStory());
        }
    }

    public enum PuzzleStatus
    {
        WIP,
        Open,
        Close
    }

    //Picks the choice (based on the choice index) to continue the ink story
    public void ChoosePuzzleChoice(int index)
    {
        PuzzleStatus chestStatus = _chest.EnterCodeDigit(index);
        if (chestStatus != PuzzleStatus.WIP)
        {
            if(chestStatus == PuzzleStatus.Open)
            {
                SetVariable(CHEST_OPEN, true);
            }

            _puzzleChoiceParent.SetActive(false);
            ChooseChoice(index);
        }
        
    }

    private void SetVariable(string varName, bool varValue)
    {
        _story.variablesState[varName] = varValue;
    }

    public void ChooseDialogueChoice(int index)
    {
        for(int i = 0; i < _story.currentChoices.Count; i++)
        {
            _dialogueChoices[i].gameObject.SetActive(false);
        }
        ChooseChoice(index);
    }
    public void OnAdvanceDialouge()
    {
        AdvanceDialogue();
        //TODO: Check if we're currently writing a sentence. If we are, load the full sentence.
    }

    private void ChooseChoice(int index)
    {
        // Kristen TODO: Play button sound
        _story.ChooseChoiceIndex(index);
        _playerCanContinue = true;
        AdvanceDialogue();
    }

    private static DialogueManager instance;

    #region Story
    private static Story _story;
    [SerializeField] private UnityEvent _dialogueEndEvent;
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
        _tags = new List<string>();    
    }

    /* 
    Advances the dialogue based on if
        (1) Player can proceed in the conversation
        (2) Player is in a conversation
        (3) There is a story
    Else, the dialogue is finished.
    */
    private void AdvanceDialogue()
    {
        if(_playerCanContinue && _playerInConvo && _story!=null)
        {
            if (_story.canContinue)
            {
                _canContinueIndicator.SetActive(false);
                _playerCanContinue = false;

                string currentSentence = _story.Continue();
                
                ParseTags();

                if(_coroutine != null)
                {
                    StopCoroutine(_coroutine);
                }
                // TODO: Account for line breaks
                _coroutine = TypeSentence(currentSentence);
                StartCoroutine(_coroutine);
                
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
                case "sfx":
                    //TODO: Figure out a way to play SFX if needed
                    //AudioManager.Instance.Play(param);
                    break;
                case "music":
                    //TODO: Figure out a way to play music if needed
                    //AudioManager.Instance.Play(param);
                    break;
            }
        }
        SetChar(characterName, emotion);

    }
    private void SetChar(string charName, string emotion)
    {
        if (charName == "")
        {
            _nameHolder.SetActive(false);
            _imageHolder.SetActive(false);
        }
        else
        {
            int characterIndex = Array.IndexOf(_characterNames, charName);
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

    /*Creates a typewriter effect when displaying a sentence. 
    Full sentence can be displayed when player presses any key that is not escape.
        sentence: the sentence to be displayed
    */
    private IEnumerator TypeSentence(string sentence)
    {
        _dialogueText.text = "";
        for(int i =1; i<sentence.Length; ++i)
        {
            _dialogueText.text = sentence.Substring(0,i) 
                                + "<color=#ffffff00>" 
                                + sentence.Substring(i)
                                + "</color>";
            yield return null;            
        }
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

    //Triggers the event/behavior that occurs after the dialogue finishes, if event exists.
    private void TriggerEndBehavior()
    {
        if(_dialogueEndEvent != null)
        {
            _dialogueEndEvent.Invoke();
        }
    }
}