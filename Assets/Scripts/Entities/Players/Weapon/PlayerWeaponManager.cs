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
    
    private List<int> weaponIDs = new List<int>();
    
    private Dictionary<int, GameObject> weaponObjects = new Dictionary<int, GameObject>();
    
    private Dictionary<int, IWeapon> weapons  = new Dictionary<int, IWeapon>();

    private float fireCooldown = 0.0f;
    
    public Vector2 Direction = Vector2.right;

    private Camera camera;

    public int CurrentWeaponID
    {
        get { return currentWeaponID; }
        private set { currentWeaponID = value; }
    }
    [SerializeField] private int currentWeaponID;

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
            var id = (int)weapon.WeaponID;
            
            weaponObject.SetActive(false);
            weaponIDs.Add(id);
    
            
            weaponObjects[id] = weaponObject;
            weapons[id] = weapon;
            
        }
        
        // TODO
        weaponObjects[(int)WeaponIDEnum.Cheap].SetActive(true);
        CurrentWeaponID = (int) WeaponIDEnum.Cheap;
        
        playerTransform = transform.parent;
        EventPublisher.PlayerPressFire += processFireCommand;

        EventPublisher.DecisionMake += OnDecisionMake;
    }

    private void OnDecisionMake(DecisionEnum decision, CharacterNameEnum bossName)
    {
        var scenename = SceneManager.GetActiveScene().name;
        if (scenename == "BlackBossScene")
        {
            PlayerConfig.HaveWeapon.Add((int)WeaponIDEnum.Black);
            if (decision == DecisionEnum.Kill)
            {
                PlayerConfig.IsWeaponBlackSpecial = true;
            }
        }
        else if (scenename == "BlueBossScene")
        {
            PlayerConfig.HaveWeapon.Add((int)WeaponIDEnum.Blue);
            if (decision == DecisionEnum.Kill)
            {
                PlayerConfig.IsWeaponBlueSpecial = true;
            }
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
    }

    void Update()
    {
        //Debug
        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            PlayerConfig.HaveWeapon.Add((int)WeaponIDEnum.Black);
        }
        if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            PlayerConfig.HaveWeapon.Add((int)WeaponIDEnum.Blue);
        }
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            PlayerConfig.IsWeaponBlackSpecial = true;
        }
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            PlayerConfig.IsWeaponBlueSpecial = true;
        }
        
        var playerPosition = playerTransform.position;
        Direction = ((Vector2) (camera.ScreenToWorldPoint(Input.mousePosition) - playerPosition)).normalized;
        transform.position = playerPosition + (Vector3)Direction * HoverDistance;
    }

    private void processFireCommand()
    {
        if (fireCooldown <= 0.0f)
        {
            fireWeapon();
            
            fireCooldown = weapons[CurrentWeaponID].MaxFireCooldown;   
        }
    }

    private void fireWeapon()
    {
        weapons[CurrentWeaponID].Fire(Direction);
        EventPublisher.TriggerPlayerFire((WeaponIDEnum)CurrentWeaponID);
    }

    public void ChangeWeaponPrev()
    {
            weaponObjects[CurrentWeaponID].SetActive(false);
            var nextID = weaponIDs[MathUtils.Mod(weaponIDs.FindIndex(x => x == CurrentWeaponID) - 1, weaponNumber)];
            CurrentWeaponID = nextID;
            while (!PlayerConfig.HaveWeapon.Contains(nextID))
            {
                nextID = weaponIDs[MathUtils.Mod(weaponIDs.FindIndex(x => x == CurrentWeaponID) - 1, weaponNumber)];
                CurrentWeaponID = nextID;
            }
            
            
            
            weaponObjects[CurrentWeaponID].SetActive(true);
    }

    public void ChangeWeaponNext()
    {
        if (weaponIDs.Count > 1)
        {
            weaponObjects[CurrentWeaponID].SetActive(false);
            var nextID = weaponIDs[MathUtils.Mod(weaponIDs.FindIndex(x => x == CurrentWeaponID) + 1, weaponNumber)];
            CurrentWeaponID = nextID;
            while (!PlayerConfig.HaveWeapon.Contains(nextID))
            {
                nextID = weaponIDs[MathUtils.Mod(weaponIDs.FindIndex(x => x == CurrentWeaponID) + 1, weaponNumber)];
                CurrentWeaponID = nextID;
            }
            weaponObjects[CurrentWeaponID].SetActive(true);
        }
    }
}
