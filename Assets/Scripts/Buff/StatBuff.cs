using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatBuff : MonoBehaviour, IBuff
{
    [SerializeField] private bool isPermanentBuff;
    [SerializeField] private float buffTimeOut;

    public bool isPermanentBuff => isPermanentBuff;
    public float buffTimeOut => buffTimeOut;

    void Start()
    {

    }

    void Update()
    {

    }

    public abstract void Apply();
}
