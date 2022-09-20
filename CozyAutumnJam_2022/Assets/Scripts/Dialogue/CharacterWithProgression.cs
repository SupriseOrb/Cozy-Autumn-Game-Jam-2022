using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "CozyAutumnJam_2022/Character/CharacterWithProgression")]
public class CharacterWithProgression : CharacterScriptableObject
{
    public enum StoryBeat
    {
        CostumeRequest = 0,
        WaitingForCostume = 1,
        GiveCostume = 2,
        AfterGiveCostume = 3,
        CannotUnderstand = 4,
        BossFirstTime = 0,
        BossAfterSpiritWorld = 1,
        BossWaiting = 2,
        PropGeneric = 0,
        PropProgressed = 1
    }
    [SerializeField] private int _currentStoryBeat;
    [SerializeField, Tooltip("Quest Index of the Character. Ken = 0, Myrtle = 1, Jowan = 2, V = 3, Chrysantha = 4")]
    private int _questIndex;
    [SerializeField] private TextAsset[] _story;   

    [SerializeField] private bool _isTranslatedDefault;
    [SerializeField] private bool _isTranslated;
    [SerializeField] private bool _isCostumeCollected;

    private void OnEnable()
    {
        _currentStoryBeat = 0;
        _isTranslated = _isTranslatedDefault;
        _isCostumeCollected = false;
    }

    public bool IsTranslated
    {
        get {return _isTranslated;}
        set {_isTranslated = value;}
    }

    public void AdvanceToStoryBeat(StoryBeat beat)
    {
        _currentStoryBeat = (int)beat;
    }

    // TODO: Account for how Diane split the story for the characters.
    public TextAsset GetStory()
    {
        return _story[_currentStoryBeat];
    }
    public int StepIndex
    {
        get {return _currentStoryBeat;}
    }
    public int QuestIndex
    {
        get {return _questIndex;}
    }
    public bool IsCostumeCollected
    {
        get {return _isCostumeCollected;}
        set {_isCostumeCollected = value;}
    }
}
