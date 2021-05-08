using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionManager : MonoBehaviour
{
    // TODO
    private PlayerWeaponManager playerWeaponManager;
    void Start()
    {
        playerWeaponManager = transform.Find("WeaponHolder").GetComponent<PlayerWeaponManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (PauseManager.GamePaused)
        {
            return;
        }
        if (Input.GetButton("Fire1"))
        {
            EventPublisher.TriggerPlayerPressFire();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerWeaponManager.ChangeWeaponPrev();
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            playerWeaponManager.ChangeWeaponNext();
        }
        
    }
}
