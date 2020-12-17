using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine {
    public State CurrentState { get; protected set; }
    public State LastState { get; protected set; }
    public void Start (State startState, dynamic options = null) {
        CurrentState = startState;
        if (options is null) {
            CurrentState.Enter ();
        } else {
            CurrentState.Enter (options);
        }
    }

    public void ChangeState (State nextState, dynamic enterOptions = null, dynamic exitOption = null) {
        LastState = CurrentState;
        if (exitOption is null) {
            LastState.Exit ();
        } else {
            LastState.Exit (exitOption);
        }
        CurrentState = nextState;
        if (enterOptions is null) {
            CurrentState.Enter ();
        } else {
            CurrentState.Enter (enterOptions);
        }
    }
}