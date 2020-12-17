using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGameState : State {
    public GameStateManager StateManager { get; }
    public BaseGameState (GameStateManager stateManager) {
        StateManager = stateManager;
    }
}