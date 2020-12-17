using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoadLevelState : BaseGameState {

    private int levelIndex;
    public GameLoadLevelState (GameStateManager stateManager) : base (stateManager) {

    }
    public override void Enter (dynamic options) {
        levelIndex = options.levelIndex;
    }
    public override void Enter () {

    }
}