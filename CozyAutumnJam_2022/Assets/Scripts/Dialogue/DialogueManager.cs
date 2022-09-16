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
    // ======================================================================
    // Interface
    // ======================================================================
    public static DialogueManager Instance
    {
        get{return instance;}
    }

    /*Starts the dialogue
        inkFile : the dialogue that will start
        endEvent : any behavior that occurs at the end of the dialogue; set to null by default
    */
    public void StartStory(TextAsset inkFile, UnityEvent endEvent = null, int _points = int.MaxValue)
    {
        if(_playerCanContinue)
        {
            _story = new Story(inkFile.text);
            _dialogueEndEvent = endEvent;
            
            // if(_points != int.MaxValue)
            // {
            //     story.variablesState["points"] = _points;
            // }

            animator.SetBool("isOpen", true);
            _playerInConvo = true;
            AdvanceDialogue();
        }
    }

    //Picks the choice (based on the choice index) to continue the ink story
    public void ChooseChoice(int index)
    {
        // //Turn off choice UI
        // for(int i = 0; i < story.currentChoices.Count; ++i)
        // {
        //     choicesArray[i].gameObject.SetActive(false);
        // }
        // story.ChooseChoiceIndex(index);
        // playerCanProceed = true;
        // AdvanceDialogue();
        
    }

    // ======================================================================
    // Implementation
    // ======================================================================
    private static DialogueManager instance;

    #region Story
    private static Story _story;
    private UnityEvent _dialogueEndEvent = null;
    private List <string> _tags;
    private IEnumerator _coroutine;
    #endregion

    #region Choices
    [SerializeField] private GameObject _dialogueChoiceParent;
    [SerializeField] private TextMeshProUGUI[] _dialogueChoices;
    [SerializeField] private GameObject _puzzleChoiceParent;

    [SerializeField] private TextMeshProUGUI[] _puzzleChoices;
    #endregion
    	
    #region Dialogue Box
    [SerializeField] private Image _charImage;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _dialogueText;
    [SerializeField] private GameObject _canContinueIndicator;
    [SerializeField] private Animator animator;

    #endregion

    #region Checks
    [SerializeField] private bool _playerCanContinue;
    [SerializeField] private bool _playerInConvo;
    #endregion

    #region Character Info
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

    void Start()
    {

    }
    void Update()
    {
        // TODO: When in dialogue, click a button to advance dialogue
        // Maybe will have to do this through events now
        // if(playerCanProceed && playerInConvo && story!=null && Input.anyKeyDown && 
        //     !(Input.GetKeyDown(KeyCode.Escape))
        //     && !(((PauseMenuManager)PauseMenuManager.instance).isGamePaused()))
        // {
        //     AdvanceDialogue();
        // }
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
                // TODO: Figure out a way to put emotion
                case "emotion":
                    emotion = param;
                    break;
                // case "points":
                //     GameManager.Instance.CalculatePoints((int) story.variablesState["points"]);
                //     break;
                // TODO: Use wwise to play an event
                case "sfx":
                    //AudioManager.Instance.Play(param);
                    break;
                case "music":
                    //AudioManager.Instance.Play(param);
                    break;
            }
        }
        SetChar(characterName, emotion);

    }
    private void SetChar(string charName, string emotion)
    {
        // TODO: if charName is empty use the narrator dialogueBox
        if (charName != "")
        {
            int characterIndex = Array.IndexOf(_characterNames, charName);
            CharacterScriptableObject currentChar = _characterInfos[characterIndex];
            _nameText.text = currentChar.Name;
            
            int emotionIndex = emotion != "" ? Array.IndexOf(_emotions, emotion) : 0;
            _charImage = currentChar.GetEmote(emotionIndex);
        }
        
    }

    private void SetEmotion(string emotion)
    {
        
    }

    //Reads and displays the choices used in the ink file
    private void ParseChoices()
    {
        if(_story.currentChoices.Count == 10)
        {
            _parseChoices(_puzzleChoices);
            _puzzleChoiceParent.SetActive(true);
        }
        else if(_story.currentChoices.Count > 0)
        {
            _parseChoices(_dialogueChoices);
            _dialogueChoiceParent.SetActive(true);
            //TODO: Set each invidiual choice active
            // Might need to create another class
        }
        else
        {
            _canContinueIndicator.SetActive(true);
            _playerCanContinue = true;
        }
    }

    private void _parseChoices(TextMeshProUGUI[] choiceTf)
    {
        for(int i = 0; i <_story.currentChoices.Count; ++i)
        {
            Choice choice = _story.currentChoices[i];
            choiceTf[i].text = choice.text;
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

            // TODO: Add functionality to show full sentence when player clicks on a button
            // if press any key that's not escape and the game is paused, complete the sentence
            // if(Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape)
            // && !(((PauseMenuManager)PauseMenuManager.instance).isGamePaused()))
            // {
            //     _dialogueText.text = sentence;
            //     ParseChoices();
            //     yield break;
            // }
        }
        ParseChoices();
    }

    //Finishes the dialogue
    private void FinishDialogue()
    {
        // TODO: Add animation bool isOpen
        animator.SetBool("isOpen", false);
        
        _nameText.text = "";
        _dialogueText.text = "";
        
        _playerInConvo = false;
        _playerCanContinue = false;
        _story = null;
        StartTimer();
        TriggerEndBehavior();
    }

    private void StartTimer()
    {
        StartCoroutine(TimeoutDialogue());
    }

    private IEnumerator TimeoutDialogue()
    {
        yield return new WaitForSeconds(0.25f);
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