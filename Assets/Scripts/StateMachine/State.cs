using System;

public class State
{
    public readonly int Id;
    public readonly Action BeginCb;
    public readonly Func<int> UpdateCb;
    public readonly Action EndCb;

    public State(int id, Action beginCb=null, Func<int> updateCb=null, Action endCb=null)
    {
        Id = id;
        BeginCb = beginCb; // get called next update after enter new state
        UpdateCb = updateCb; // return next state, if stay on this state return self id
        EndCb = endCb; // get called next update after exist this state
    }
}
