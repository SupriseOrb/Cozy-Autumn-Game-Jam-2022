using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    static private QuestManager _instance;
    static public QuestManager Instance { get { return _instance;}}
    [SerializeField] private bool _allQuestsComplete;
    
    [System.Serializable]
    public class HumanQuest
    {
        public CharacterWithProgression _currentQuestHuman;
        public bool[] _humanQuestStepCompleted;
        public bool _humanQuestComplete;
        public bool _humanQuestStarted;
    }

    [System.Serializable]
    public class SpiritQuest
    {
        public CharacterWithProgression _currentQuestSpirit;
        public GameObject _oracleCard;
        public bool[] _spiritQuestStepCompleted;
        public bool _spiritQuestComplete;
        public bool _spiritQuestStarted;
    }

    [System.Serializable]
    public class BossQuest
    {
        public CharacterWithProgression _currentQuestBoss;
        public bool[] _bossQuestStepCompleted;
        public bool _bossQuestStarted;
        public bool _bossQuestComplete;
    }

    [SerializeField] private HumanQuest[] _humanQuestList;
    [SerializeField] private SpiritQuest[] _spiritQuestList;
    [SerializeField] private BossQuest[] _bossQuestList;
    [SerializeField] private GameObject _myrtleIntroGO;
    [SerializeField] private GameObject _myrtleGO;
    [SerializeField] private GameObject _bossGO;
    [SerializeField] private GameObject _disappearShelf;

    public HumanQuest[] HumanQuestList
    {
        get {return _humanQuestList;}
    }
    public SpiritQuest[] SpiritQuestList
    {
        get {return _spiritQuestList;}
    }
    public BossQuest[] BossQuestList
    {
        get {return _bossQuestList;}
    }

    void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        } 
    }

    public void CompleteQuestStepHuman(CharacterWithProgression humanChar)
    {
        HumanQuestList[humanChar.QuestIndex]._humanQuestStepCompleted[humanChar.StepIndex] = true;
        int _stepCount = HumanQuestList[humanChar.QuestIndex]._humanQuestStepCompleted.Length;
        if (HumanQuestList[humanChar.QuestIndex]._humanQuestComplete == true)
        {
            return;
        }
        else if (HumanQuestList[humanChar.QuestIndex]._humanQuestComplete != true)
        {
            if (humanChar.StepIndex == 0)
            {
                Debug.Log("Started Quest!");
                HumanQuestList[humanChar.QuestIndex]._humanQuestStarted = true;
                
                HumanQuestList[humanChar.QuestIndex]._currentQuestHuman.AdvanceToStoryBeat(CharacterWithProgression.StoryBeat.WaitingForCostume);
            }
            else if (humanChar.StepIndex == 1)
            {
                if (humanChar.IsCostumeCollected == true)
                {
                    Debug.Log("Got the human costume!");
                    HumanQuestList[humanChar.QuestIndex]._currentQuestHuman.AdvanceToStoryBeat(CharacterWithProgression.StoryBeat.GiveCostume);
                }
                else 
                {
                    HumanQuestList[humanChar.QuestIndex]._humanQuestStepCompleted[humanChar.StepIndex] = false;
                    Debug.Log("No human costume...");
                }
            }
            else if (humanChar.StepIndex == 2)
            {
                Debug.Log("Gave the human costume!");
                HumanQuestList[humanChar.QuestIndex]._currentQuestHuman.AdvanceToStoryBeat(CharacterWithProgression.StoryBeat.AfterGiveCostume);
                HumanQuestList[humanChar.QuestIndex]._humanQuestComplete = true;
            }
            else if (humanChar.StepIndex == 3)
            {
                Debug.Log("Human Flavor Text uwu");
                if (HumanQuestList[humanChar.QuestIndex]._humanQuestComplete != true)
                {
                    HumanQuestList[humanChar.QuestIndex]._humanQuestComplete = true; 
                }
            }
            
        }
    }
    public void CompleteQuestStepSpirit(CharacterWithProgression spiritChar)
    {
        _spiritQuestList[spiritChar.QuestIndex]._spiritQuestStepCompleted[spiritChar.StepIndex] = true;
        if (_spiritQuestList[spiritChar.QuestIndex]._spiritQuestComplete == true)
        {
            return;
        }
        else if (_spiritQuestList[spiritChar.QuestIndex]._spiritQuestComplete != true)
        {
            if (spiritChar.StepIndex == 0)
            {
                Debug.Log("Started Spirit Quest!");
                _spiritQuestList[spiritChar.QuestIndex]._spiritQuestStarted = true;
                _spiritQuestList[spiritChar.QuestIndex]._currentQuestSpirit.AdvanceToStoryBeat(CharacterWithProgression.StoryBeat.WaitingForCostume);
                //Get script component of ability and set it active (likely need to edit this)
                _spiritQuestList[spiritChar.QuestIndex]._oracleCard.GetComponent<IAbility>().ObtainAbility();
            }
            else if (spiritChar.StepIndex == 1)
            {
                if (spiritChar.IsCostumeCollected == true)
                {
                    Debug.Log("Got the spirit costume!");
                    _spiritQuestList[spiritChar.QuestIndex]._currentQuestSpirit.AdvanceToStoryBeat(CharacterWithProgression.StoryBeat.GiveCostume);
                }
                else
                {
                    _spiritQuestList[spiritChar.QuestIndex]._spiritQuestStepCompleted[spiritChar.StepIndex] = false;
                    Debug.Log("No spirit costume...");
                }
            }
            else if (spiritChar.StepIndex == 2)
            {
                Debug.Log("Gave the spirit costume!");
                _spiritQuestList[spiritChar.QuestIndex]._currentQuestSpirit.AdvanceToStoryBeat(CharacterWithProgression.StoryBeat.AfterGiveCostume);
                _spiritQuestList[spiritChar.QuestIndex]._spiritQuestComplete = true; 
            }
            else if (spiritChar.StepIndex == 3)
            {
                Debug.Log("Spirit Flavor Text uwu");
                if (_spiritQuestList[spiritChar.QuestIndex]._spiritQuestComplete != true)
                {
                    _spiritQuestList[spiritChar.QuestIndex]._spiritQuestComplete = true; 
                }
            }
               
        }
        



    }

    //Used by Boss and Intro Myrtle (different functionality)
    public void CompleteQuestStepBoss(CharacterWithProgression bossChar)
    {
        _bossQuestList[bossChar.QuestIndex]._bossQuestStepCompleted[bossChar.StepIndex] = true;
        int _stepCount = _bossQuestList[bossChar.QuestIndex]._bossQuestStepCompleted.Length;
        if (_allQuestsComplete == true)
        {
            //Indicate the game is over? This would happen after the final dialogue from the boss
        }
        if (_spiritQuestList[bossChar.QuestIndex]._spiritQuestComplete == true)
        {
            return;
        }
        else if (HumanQuestList[bossChar.QuestIndex]._humanQuestComplete != true)
        {
            if (bossChar.StepIndex == 0)
            {
                Debug.Log("Started Boss Quest!");
                _bossQuestList[bossChar.QuestIndex]._bossQuestStarted = true;
                _bossQuestList[bossChar.QuestIndex]._currentQuestBoss.AdvanceToStoryBeat(CharacterWithProgression.StoryBeat.BossAfterSpiritWorld);
                _bossQuestList[bossChar.QuestIndex]._bossQuestComplete = true;
            }
            if (bossChar.StepIndex == 1)
            {
                Debug.Log("Played Boss Text!");
                _bossQuestList[bossChar.QuestIndex]._bossQuestComplete = true;
                if (_allQuestsComplete == true)
                {
                    _bossQuestList[bossChar.QuestIndex]._currentQuestBoss.AdvanceToStoryBeat(CharacterWithProgression.StoryBeat.BossEndGame);
                }
            }
        }   

    }

    //TODO: Instead of calling this function directly from the UnityEvent, instead call through the animation transition
    public void IntroTeleport(DoubleVector2Var teleDistance)
    {
        _myrtleIntroGO.transform.position = teleDistance.ValueFirst;
        PlayerScript.Instance.transform.position = teleDistance.ValueSecond;
    }

    //TODO: Add Vfx & call through animation transition instead
    public void IntroEnd()
    {
        _disappearShelf.SetActive(false);
        _myrtleIntroGO.SetActive(false);
        _myrtleGO.SetActive(true);
    }

    public void CheckForCompletion()
    {
        int _humanQuestCount = _humanQuestList.Length;
        int _spiritQuestCount = _spiritQuestList.Length;
        int _bossQuestCount = _bossQuestList.Length;

        int _humanQuestsComplete = 0;
        int _spiritQuestsComplete = 0;
        int _bossQuestsComplete = 0;

        foreach (HumanQuest hquest in _humanQuestList)
        {
            if (hquest._humanQuestComplete == true)
            {
                _humanQuestsComplete++;
            }
        }

        foreach (SpiritQuest squest in _spiritQuestList)
        {
            if (squest._spiritQuestComplete == true)
            {
                _spiritQuestsComplete++;
            }
        }

        foreach (BossQuest bquest in _bossQuestList)
        {
            if (bquest._bossQuestComplete == true)
            {
                _bossQuestsComplete++;
            }
        }

        if (_humanQuestsComplete == _humanQuestCount && _spiritQuestsComplete == _spiritQuestCount && 
            _bossQuestsComplete == _bossQuestCount)
        {
            _allQuestsComplete = true;
        }

    }

        //you might need to add some logic to prevent the skipping of story beats if the player were to pick up a costume too early
        //if stepindex is 0, aka the start. set currentquesthuman story beat to waiting for costume
        //if step index is 1, set currentquestspirit story beat to waitign for costuem
            //also give player the ability associated with this quest
            //also check to see if index 2 is set to true (aka you already picked up the costume) then skip the index 2 part
        //if step index is 2, this is when you pick up the spirit costume, set spirit story beat to givecostume
        //if step index is 3, this is giving the spirit costume,  set spirit story beat to aftergivecostume
        //if step index is 4, this is when you pick up the human costume, set human story beat to give costume
        //if step index is 5, this is giving the human costume, set human story beat to aftergivecostume
        //if step index is 6, the quest is complete

        //IMPORTANT NOTE, the boss' quest will be different than the others so it will not work with this setup
        //also there may be items that the player gets that I forgot to mention
}
