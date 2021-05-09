using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatBuff : IBuff
{
    protected string buffName;
    protected bool isPermanentBuff;
    protected float maxBuffDuration;
    protected float buffDuration;
    protected bool isApplied;

    protected bool isFinished;

    public abstract void End();
    public abstract void Apply(CharacterStats characterStats);
}

//public abstract class StatBuff_old : IBuff
//{
//    protected string buffName;
//    protected bool isPermanentBuff;
//    protected float maxBuffDuration;
//    protected float buffDuration;
//    protected bool isApplied;
//    //protected int priority;

//    protected bool isFinished;

//    public void Tick(float delta)
//    {
//        if (!isPermanentBuff)
//        {
//            buffDuration -= delta;
//            if (buffDuration <= 0)
//            {
//                isFinished = true;
//                End();
//            }
//        }
//    }


//    public abstract void End();
//    public abstract void Apply(CharacterStats characterStats);
//}
