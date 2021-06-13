using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class GuardianManager : MonoBehaviour
{
    private EnvManager envManager;
    public Animation anim;
    public GameObject tileClearerPrefab;

    public ColorEnum color;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
        // envManager.Clear(color);
        EventPublisher.GuardianCall += OnGuardianCall;
    }

    private void OnDestroy()
    {
        EventPublisher.GuardianCall -= OnGuardianCall;
    }

    private void OnGuardianCall(ColorEnum colorEnum)
    {
        if (colorEnum == color)
        {
            anim.Play();
            print("guardian call");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Clear()
    {
        Instantiate(tileClearerPrefab, transform.position, Quaternion.Euler(Vector3.zero));
        print("clear" + color); 
       
    }
}
