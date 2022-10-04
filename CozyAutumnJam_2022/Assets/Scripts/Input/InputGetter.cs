using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class InputGetter : Unit
{
    [DoNotSerialize]
    public ControlInput inputTrigger;
    [DoNotSerialize]
    public ControlOutput outputTrigger;
    [DoNotSerialize]
    public ValueOutput XValue;
    /*
    [DoNotSerialize]
    public ValueOutput XValue;
    [DoNotSerialize]
    public ValueOutput YValue;
    */
    [DoNotSerialize]
    public ValueInput InputParent;
    [DoNotSerialize]
    public ValueOutput OutputVector;

    private InputManager inputted;
    private Vector2 DirectionVector;

    private float XVal;
    private float YVal;
    protected override void Definition()
    {
        inputTrigger = ControlInput("inputTrigger", (flow)=>
        {
            inputted = flow.GetValue<InputManager>(InputParent);
            DirectionVector = inputted.MovementInput;
            //YVal = flow.GetValue<float>(YValue);
            return outputTrigger;
        });
        outputTrigger = ControlOutput("outputTrigger");
        InputParent = ValueInput<InputManager>("Input Getter", null);

        OutputVector = ValueOutput<Vector2>("Direction", (flow)=> {return DirectionVector;});
        Succession(inputTrigger, outputTrigger);
    }
}
