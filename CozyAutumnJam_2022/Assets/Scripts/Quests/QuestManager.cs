using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    static private QuestManager _instance;
    static public QuestManager Instance { get { return _instance;}}
    
    [System.Serializable]
    public class Quest
    {
        public CharacterWithProgression _currentQuestHuman;
        public CharacterWithProgression _currentQuestSpirit;
        //add a variable for the oracle card associated with this quest
        public bool[] _questStepCompleted;
        public bool _questComplete;
        public bool _questStarted;
    }

    [SerializeField] private Quest[] _questList;
    
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

    public void CompleteQuestStep(int questIndex, int stepIndex) //figure out to pass 2 ints in an event
    {
        //if not true already
        _questList[questIndex]._questStepCompleted[stepIndex] = true;
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

}
