using System;
using System.Collections;
using System.Collections.Generic;
using Players.Weapon;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class PlayerWeaponManager : MonoBehaviour
{
    public float HoverDistance;

    private Transform playerTransform;
    
    private int weaponNumber;
    private Dictionary<WeaponIDEnum, IWeapon> weaponDict  = new Dictionary<WeaponIDEnum, IWeapon>();

    private float fireCooldown = 0.0f;
    
    public Vector2 Direction = Vector2.right;

    private Camera camera;

    public WeaponIDEnum CurrentWeaponID
    {
        get { return currentWeaponID; }
        private set { currentWeaponID = value; }
    }
    [SerializeField] private WeaponIDEnum currentWeaponID;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        
        weaponNumber = transform.childCount;;
        //print( weaponNumber );
        for (int i = 0; i < weaponNumber; i++)
        {
            var weaponObject = transform.GetChild(i).gameObject;
            var weapon = weaponObject.GetComponent<IWeapon>();
            var id = weapon.WeaponID;

            // switch (id)
            // {
            //     case WeaponIDEnum.Black:
            //         
            //         break;
            //     case WeaponIDEnum.Blue:
            //         break;
            //     case WeaponIDEnum.Cheap:
            //         break;
            // }
            weaponDict[id] = weapon;
            weaponDict[id].Disable();

        }
        
        playerTransform = transform.parent;
        EventPublisher.PlayerPressFire += processFireCommand;

        EventPublisher.DecisionMake += OnDecisionMake;
    }

    public void SetWeapon()
    {
        
    }

    private void OnDecisionMake(DecisionEnum decision, CharacterNameEnum bossName)
    {
        switch (bossName)
        {
            case CharacterNameEnum.Black:
                AddWeapon(WeaponIDEnum.Black);
                if (decision == DecisionEnum.Kill)
                {
                    PlayerConfig.IsWeaponBlackSpecial = true;
                }
                else if (decision == DecisionEnum.Mercy)
                {
                    PlayerConfig.HaveBlackResemblance = true;
                }
                
                break;
            case CharacterNameEnum.Blue:
                AddWeapon(WeaponIDEnum.Blue);
                if (decision == DecisionEnum.Kill)
                {
                    PlayerConfig.IsWeaponBlueSpecial = true;
                }
                else if (decision == DecisionEnum.Mercy)
                {
                    PlayerConfig.HaveBlueResemblance = true;
                }

                break;
        }
                
    }

    private void OnDestroy()
    {
        EventPublisher.PlayerPressFire -= processFireCommand;
        EventPublisher.DecisionMake -= OnDecisionMake;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (fireCooldown > 0.0f)
        {
            fireCooldown -= Time.fixedDeltaTime;
        }
        
        var playerPosition = playerTransform.position;
        Direction = ((Vector2) (camera.ScreenToWorldPoint(Input.mousePosition) - playerPosition)).normalized;
        transform.position = playerPosition + (Vector3)Direction * HoverDistance;
    }

    void Update()
    {
        //Debug
        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            PlayerConfig.HaveWeapon.Add(WeaponIDEnum.Black);
        }
        if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            PlayerConfig.HaveWeapon.Add(WeaponIDEnum.Blue);
        }
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            PlayerConfig.IsWeaponBlackSpecial = true;
        }
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            PlayerConfig.IsWeaponBlueSpecial = true;
        }
    }

    private void processFireCommand()
    {
        if (fireCooldown <= 0.0f)
        {
            fireWeapon();
            
            fireCooldown = weaponDict[CurrentWeaponID].MaxFireCooldown;   
        }
    }

    private void fireWeapon()
    {
        weaponDict[CurrentWeaponID].Fire(Direction);
        EventPublisher.TriggerPlayerFire(CurrentWeaponID);
    }

    public void ChangeWeaponNext()
    {
        weaponDict[CurrentWeaponID].Disable();
        var currWeaponHaveWeaponIndex = PlayerConfig.HaveWeapon.FindLastIndex(x => x == currentWeaponID);
        currentWeaponID =
            PlayerConfig.HaveWeapon[MathUtils.Mod(currWeaponHaveWeaponIndex+1, PlayerConfig.HaveWeapon.Count)];
        weaponDict[currentWeaponID].Enable();
    }

    public void SetWeapon(WeaponIDEnum weaponId)
    {
        if (!PlayerConfig.HaveWeapon.Contains(weaponId))
        {
            print("Cannot set not have weapon" + weaponId);
            return;
        }
        foreach(var kv in weaponDict)
        {
            kv.Value.Disable();
        }
        weaponDict[weaponId].Enable();
    }

    public void AddWeapon(WeaponIDEnum weaponId)
    {
        if (!PlayerConfig.HaveWeapon.Contains(weaponId))
        {
            PlayerConfig.HaveWeapon.Add(weaponId);
        }
        SetWeapon(weaponId);
    }
}
