using System.Collections;
using System.Collections.Generic;
using Players.Weapon;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    public float HoverDistance;

    private Transform playerTransform;
    
    private int weaponNumber;
    
    private List<int> weaponIDs = new List<int>();
    
    private Dictionary<int, GameObject> weaponObjects = new Dictionary<int, GameObject>();
    
    private Dictionary<int, IWeapon> weapons  = new Dictionary<int, IWeapon>();

    private float fireCooldown = 0.0f;
    
    public int CurrentWeaponID
    {
        get { return currentWeaponID; }
        private set { currentWeaponID = value; }
    }
    [SerializeField] private int currentWeaponID;

    // Start is called before the first frame update
    void Start()
    {
        weaponNumber = transform.childCount;

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
        weaponObjects[(int)WeaponIDEnum.Black].SetActive(true);
        CurrentWeaponID = (int) WeaponIDEnum.Black;
        
        playerTransform = transform.parent;
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

    void Update()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        transform.position = playerTransform.position + Vector3.Normalize(mousePosition - playerTransform.position) * HoverDistance;
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
        weapons[CurrentWeaponID].Fire();
        EventPublisher.TriggerPlayerFire();
    }
}
