using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats
{
    [SerializeField] private float baseValue;

    private List<float> modifiers = new List<float>();

    public int GetValue()
    {
        int finalValue = baseValue;

        modifiers.ForEach(x => finalValue += x);
        return finalValue;
    }

    public void AddModifier(int mod)
    {
        if (mod != 0) modifiers.Add(mod);
    }

    public void RemoveModifier(int mod)
    {
        if (mod != 0) modifiers.Remove(mod);
    }
}
