using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    [SerializeField] private float maxFireCooldown = 0.5f;
    [SerializeField] private GameObject bulletObject;
    private float fireCooldown = 0.0f;
    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GetComponent<Transform>();
        EventPublisher.PlayerPressFire += processFireCommand;
    }

    private void OnDestroy()
    {
        EventPublisher.PlayerPressFire -= processFireCommand;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (fireCooldown > 0.0f)
        {
            fireCooldown -= Time.fixedDeltaTime;
        }
    }

    private void processFireCommand()
    {
        if (fireCooldown <= 0.0f)
        {
            fireWeapon();
            fireCooldown = maxFireCooldown;   
        }
    }

    private void fireWeapon()
    {
        var bullet = Instantiate(bulletObject, playerTransform.position, Quaternion.Euler(Vector3.zero));
        EventPublisher.TriggerPlayerFire();
    }
}
