using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameState : BaseGameState {

    private GameManager manager;
    private UIManager uIManager;
    public InGameState (GameStateManager stateManager) : base (stateManager) {
        manager = stateManager.Manager;
        uIManager = manager.UIManager;
    }

    public override void Enter (dynamic options) {

    }
    public override void Enter () {

    }
}