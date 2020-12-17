using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager {
    public GameManager Manager { get; }
    public StateMachine Machine { get; }

    public GameStateManager (GameManager manager) {
        Manager = manager;
        Machine = new StateMachine ();
    }
}