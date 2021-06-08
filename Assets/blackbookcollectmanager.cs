using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blackbookcollectmanager : MonoBehaviour
{
    public gateManager GateManager;

    public PlayerWeaponManager WeaponManager;

    public catdialogmanager catdi;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerConfig.HaveWeapon.Add((int)WeaponIDEnum.Black);
            GateManager.FinishRoom();
            WeaponManager.ChangeWeaponNext();
            Destroy(gameObject);
            catdi.SetText("Cat: What is that book bro?");
        }
    }

    
}
