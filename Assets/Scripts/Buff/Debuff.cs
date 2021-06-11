using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Debuff
{
    protected ColorEnum debuffColor;
    protected float maxBuffDuration;
    protected float buffDuration;
    protected bool isActive;
    
    public Debuff(ColorEnum color, float maxBuffDuration)
    {
        debuffColor = color;
        this.maxBuffDuration = maxBuffDuration;
    }

    public void Tick(float dt)
    {
        buffDuration -= dt;
        if (buffDuration <= 0)
        {
            isActive = false;
        }
    }

    public void Apply()
    {
        buffDuration = maxBuffDuration;
        isActive = true;
    }
}
