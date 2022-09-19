using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestInfoScriptableObject", menuName = "CozyAutumnJam_2022/QuestInfoScriptableObject", order = 0)]
public class QuestInfo : ScriptableObject
{
    [SerializeField] private int _questIndex;
    [SerializeField] private int _stepIndex;

    public int QuestIndex
    {
        get {return _questIndex;}
        set {_questIndex = value;}
    }
    public int StepIndex
    {
        get {return _stepIndex;}
        set {_stepIndex = value;}
    }

}
