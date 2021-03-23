using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro.EditorUtilities;
using UnityEngine;

public class StateMachine
{
    public int StateId { get; private set; }

    private State state;
    private int defaultStateId;
    private Dictionary<int, State> states = new Dictionary<int, State>();
    private bool isJustInit;

    public void Initialize(int startId, int defaultId)
    {
        isJustInit = true;
        SetState(startId);
        state = states[startId];
        defaultStateId = defaultId;
    }

    public void Process()
    {
        var prevState = state;
        if (prevState.Id != StateId)
        {
            prevState.EndCb?.Invoke();

            state = states[StateId];

            state.BeginCb?.Invoke();
        } 
        else if (isJustInit)
        {
            if (!(prevState.BeginCb is null))
                state.BeginCb();
            isJustInit = false;
        }

        SetState(state.UpdateCb?.Invoke() ?? defaultStateId); // on update return next state
    }

    public void AddState(State state)
    {
        states[state.Id] = state;
    }

    public void SetState(int stateId)
    {
        StateId = stateId;
    }
}
