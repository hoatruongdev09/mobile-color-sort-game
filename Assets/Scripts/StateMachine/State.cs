using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State {
    public virtual bool IsSuspended { get; set; }

    public virtual void Enter () { }
    public virtual void Enter (dynamic options) { Enter (); }
    public virtual void Update () { if (IsSuspended) { return; } }
    public virtual void Exit () { }
    public virtual void Exit (dynamic options) { Exit (); }

    public virtual void EnterAsSubState () { }
    public virtual void EnterAsSubState (dynamic options) { EnterAsSubState (); }

    public virtual void Suspend () { IsSuspended = true; }
    public virtual void Suspend (dynamic options) { Suspend (); }

    public virtual void Continue () { IsSuspended = false; }
    public virtual void Continue (dynamic options) { Continue (); }
}