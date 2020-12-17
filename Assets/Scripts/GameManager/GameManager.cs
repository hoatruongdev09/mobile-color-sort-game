using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public UIManager UIManager => uIManager;
    [SerializeField] private UIManager uIManager;
    private GameStateManager stateManager;

    public void Start () {
        stateManager = new GameStateManager (this);
    }
}