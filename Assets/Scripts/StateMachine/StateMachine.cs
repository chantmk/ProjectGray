using System;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro.EditorUtilities;
using UnityEngine;

public class StateMachine <T>
{
    public T CurrentState { get; private set; }
    public T PreviousState { get; private set; }
    public T NextState { get; private set; }

    public StateMachine(T entryState)
    {
        PreviousState = entryState;
        CurrentState = entryState;
        NextState = entryState;
    }

    public void SetNextState(T state)
    {
        NextState = state;
    }

    public void ChangeState()
    {
        PreviousState = CurrentState;
        CurrentState = NextState;
    }
}
