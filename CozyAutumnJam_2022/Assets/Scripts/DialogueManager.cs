using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using Ink.Runtime;
using TMPro;
// public class DialogueManager : MonoBehaviour
// {
//     // ======================================================================
//     // Interface
//     // ======================================================================
//     public static DialogueManager Instance
//     {
//         get{return instance;}
//     }

//     /*Starts the dialogue
//         inkFile : the dialogue that will start
//         endEvent : any behavior that occurs at the end of the dialogue; set to null by default
//     */
//     public void StartStory(TextAsset inkFile, UnityEvent endEvent = null, int _points = int.MaxValue)
//     {
//         if(playerCanProceed)
//         {
//             dialogueBox.SetActive(true);
//             dialogueEndEvent = endEvent;
//             textAsset = inkFile;
//             story = new Story(inkFile.text);
//             if(_points != int.MaxValue)
//             {
//                 story.variablesState["points"] = _points;
//             }
//             //animator.SetBool("isOpen", true);
//             playerInConvo = true;
//             AdvanceDialogue();
//         }
//     }

//     //Picks the choice (based on the choice index) to continue the ink story
//     public void ChooseChoice(int index)
//     {
//         //Turn off choice UI
//         for(int i = 0; i < story.currentChoices.Count; ++i)
//         {
//             choicesArray[i].gameObject.SetActive(false);
//         }
//         story.ChooseChoiceIndex(index);
//         playerCanProceed = true;
//         AdvanceDialogue();
        
//     }

//     // ======================================================================
//     // Implementation
//     // ======================================================================
//     private static DialogueManager instance;

//     #region Story
//     private static Story story;
//     private TextAsset textAsset;
//     private UnityEvent dialogueEndEvent = null;
//     private List <string> tags;
//     [SerializeField] private ChoiceButton[] choicesArray;
//     private IEnumerator coroutine;
//     #endregion
    	
//     #region Dialogue Box
//     [SerializeField] private Image charImage;
//     [SerializeField] private TextMeshProUGUI nameText;
//     [SerializeField] private TextMeshProUGUI dialogueText;
//     [SerializeField] private GameObject canContinueIndicator;
//     [SerializeField] private GameObject dialogueBox;
//     #endregion

//     #region Checks
//     [SerializeField] private bool playerCanProceed;
//     [SerializeField] private bool playerInConvo;
//     #endregion

//     #region Animation
//     //[SerializeField] private Animator animator;
//     #endregion

//     #region Character Info
//     //Size 2, 0 left char, 1 right char
//     [SerializeField] private Image[] fullBodyImages;
//     [SerializeField] private Character[] characterArray;
//     private Dictionary<string,Sprite> caatroxEmotes;
//     private Dictionary<string,Sprite> kayleEmotes;
//     #endregion

//     void Awake()
//     {
//         if (instance == null)
//         {
//             instance = this;
//         }
//         else
//         {
//             Destroy(this);
//         }
//         tags = new List<string>();    
//     }
//     void Start()
//     {
//         nameText.text = "";
//         dialogueText.text = "";
//         caatroxEmotes = characterArray[0].CharacterEmotes;
//         kayleEmotes = characterArray[1].CharacterEmotes;
//     }

//     void Update()
//     {
//         if(playerCanProceed && playerInConvo && story!=null && Input.anyKeyDown && 
//             !(Input.GetKeyDown(KeyCode.Escape))
//             && !(((PauseMenuManager)PauseMenuManager.instance).isGamePaused()))
//         {
//             AdvanceDialogue();
//         }
//     }

//     /* 
//     Advances the dialogue based on if
//         (1) Player can proceed in the conversation
//         (2) Player is in a conversation
//         (3) There is a story
//     Else, the dialogue is finished.
//     */
//     public void AdvanceDialogue()
//     {
//         if(playerCanProceed && playerInConvo && story!=null)
//         {
//             if (story.canContinue)
//             {
//                 canContinueIndicator.SetActive(false);
//                 playerCanProceed = false;
//                 string currentSentence = story.Continue();
//                 ParseTags();
//                 if(coroutine != null)
//                 {
//                     StopCoroutine(coroutine);
//                 }
//                 coroutine = TypeSentence(currentSentence);
//                 StartCoroutine(coroutine);
                
//             }
//             else
//             {
//                 FinishDialogue();
//             }
//         }
                
//     }

//     // Reads and separate the tags used in the ink file
//     private void ParseTags()
//     {
//         tags = story.currentTags;

//         foreach(string t in tags)
//         {
//             string prefix = t.Split(' ')[0];
//             string param = t.Substring(prefix.Length+1);
//             switch(prefix.ToLower())
//             {
//                 case "char":
//                     SetChar(param);
//                     break;
//                 case "points":
//                     GameManager.Instance.CalculatePoints((int) story.variablesState["points"]);
//                     break;
//                 case "sfx":
//                     AudioManager.Instance.Play(param);
//                     break;
//                 case "music":
//                     AudioManager.Instance.Play(param);
//                     break;
//                 case "enter":
//                     if(param == "caatrox")
//                     {
//                         fullBodyImages[0].enabled = true;
//                     }
//                     else if (param == "kayle")
//                     {
//                         fullBodyImages[1].enabled = true;
//                     }
//                     break;
//                 case "leave":
//                     if(param == "caatrox")
//                     {
//                         fullBodyImages[0].enabled = false;
//                     }
//                     else if(param == "kayle")
//                     {
//                         fullBodyImages[1].enabled = false;
//                     }
//                     break;
//             }
//         }

//     }
    
//     private void SetChar(string _name)
//     {
//         if (_name.Split(' ')[0] == characterArray[1].CharacterName)
//         {
//             if(!MenuManager.instance.isAccessibleFontOn)
//             {
//                 dialogueText.font = characterArray[1].CharacterFont;
//             }
//             if(_name.Split(' ').Length > 1)
//             {
//                 if (kayleEmotes.ContainsKey(_name.Split(' ')[1]))
//                 {
//                     fullBodyImages[1].sprite = kayleEmotes[_name.Split(' ')[1]];
//                 }
//                 else
//                 {
//                     Debug.LogWarning("Does not contain key " + _name.Split(' ')[1]);
//                 }
//             }
//             else
//             {
//                 fullBodyImages[1].sprite = characterArray[1].CharacterFullBody;
//             }            
//             nameText.text = "Kayle";
//             charImage.sprite = characterArray[1].CharacterProfileSprite;
//             fullBodyImages[1].color = Color.white;
//             fullBodyImages[0].color = Color.gray;
//         }
//         else if (_name.Split(' ')[0] == characterArray[0].CharacterName)
//         {
//             if(!MenuManager.instance.isAccessibleFontOn)
//             {
//                 dialogueText.font = characterArray[0].CharacterFont;
//             }
//             if(_name.Split(' ').Length > 1)
//             {
//                 if (caatroxEmotes.ContainsKey(_name.Split(' ')[1]))
//                 {
//                     fullBodyImages[0].sprite = caatroxEmotes[_name.Split(' ')[1]];
//                 }
//                 else
//                 {
//                     Debug.LogWarning("Does not contain key " + _name.Split(' ')[1]);
//                 }
//             }
//             else
//             {
//                 fullBodyImages[0].sprite = characterArray[0].CharacterFullBody;
//             }
//             nameText.text = "Caatrox";
//             charImage.sprite = characterArray[0].CharacterProfileSprite;
//             fullBodyImages[0].color = Color.white;
//             fullBodyImages[1].color = Color.gray;
//         }
//         else
//         {
//             if(!MenuManager.instance.isAccessibleFontOn)
//             {
//                 dialogueText.font = characterArray[2].CharacterFont;
//             }
//             nameText.text = "Narrator";
//             charImage.sprite = characterArray[2].CharacterProfileSprite;
//             fullBodyImages[0].color = Color.gray;
//             fullBodyImages[1].color = Color.gray;  
//         }

//     }

//     //Reads and displays the choices used in the ink file
//     private void ParseChoices()
//     {
//         if(story.currentChoices.Count > 0)
//         {
//             for(int i = 0; i <story.currentChoices.Count; ++i)
//             {
//                 Choice choice = story.currentChoices[i];
//                 choicesArray[i].setText(choice.text);
//             }
            
//             //Turn ON choice UI
//             for(int i = 0; i < story.currentChoices.Count; ++i)
//             {
//                 choicesArray[i].gameObject.SetActive(true);
//             }
//         }
//         else
//         {
//             canContinueIndicator.SetActive(true);
//             playerCanProceed = true;
//         }

//     }

//     /*Creates a typewriter effect when displaying a sentence. 
//     Full sentence can be displayed when player presses any key that is not escape.
//         sentence: the sentence to be displayed
//     */
//     private IEnumerator TypeSentence(string sentence)
//     {
//         dialogueText.text = "";
//         for(int i =1; i<sentence.Length; ++i)
//         {
//             dialogueText.text = sentence.Substring(0,i) 
//                                 + "<color=#ffffff00>" 
//                                 + sentence.Substring(i)
//                                 + "</color>";
//             yield return null;

//             if(Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape)
//             && !(((PauseMenuManager)PauseMenuManager.instance).isGamePaused()))
//             {
//                 dialogueText.text = sentence;
//                 ParseChoices();
//                 yield break;
//             }
//         }
//         ParseChoices();
//     }

//     //Finishes the dialogue
//     private void FinishDialogue()
//     {
//         //animator.SetBool("isOpen", false);
        
//         nameText.text = "";
//         dialogueText.text = "";
//         playerInConvo = false;
//         playerCanProceed = false;
//         story = null;
//         textAsset = null;
//         StartTimer();
//         TriggerEndBehavior();

//     }

//     private void StartTimer()
//     {
//         StartCoroutine(TimeoutDialogue());
//     }

//     private IEnumerator TimeoutDialogue()
//     {
//         yield return new WaitForSeconds(0.25f);
//         playerCanProceed = true;
//     }

//     //Triggers the event/behavior that occurs after the dialogue finishes, if event exists.
//     private void TriggerEndBehavior()
//     {
//         dialogueBox.SetActive(false);
//         if(dialogueEndEvent != null)
//         {
//             dialogueEndEvent.Invoke();
//         }
//     }
// }