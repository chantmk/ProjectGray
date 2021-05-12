using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;
using MathUtils = Utils.MathUtils;

public class BlackPillarManager : MonoBehaviour
{
    public Sprite[] sprites = new Sprite[3];
    public GameObject blackHolePrefab;
    public Transform Status;
    public RadiusUI radiusUI;
    
    private int level;
    private readonly int maxLevel = 2;

    private float charge;
    private readonly float maxCharge = 1;
    [SerializeField] private float dischargeRate; 
    private bool isAddCharge;

    private float dischargeCountDown;
    [SerializeField] private float maxDischargeCountDown;

    private bool isPlayerInArea;
    
    private SpriteRenderer spriteRenderer;
    
    private StateMachine<EnvStateEnum> stateMachine;
    private bool isActivateEnd;

    // Start is called before the first frame update
    void Start()
    {
        stateMachine = new StateMachine<EnvStateEnum>(EnvStateEnum.ZeroCharge);

        level = 0;
        charge = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        EventPublisher.PlayerFire += OnPlayerPressFire;
    }

    public void OnDestroy()
    {
        EventPublisher.PlayerFire -= OnPlayerPressFire;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var otherGameObject = other.gameObject;
        if (otherGameObject.CompareTag("Player"))
        {
            isPlayerInArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var otherGameObject = other.gameObject;
        if (otherGameObject.CompareTag("Player"))
        {
            isPlayerInArea = false;
        }
    }

    public void OnPlayerPressFire(WeaponIDEnum weaponID)
    {
        if (isPlayerInArea && weaponID == WeaponIDEnum.Black)
            isAddCharge = true;
    }

    private void ChangeCharge(float amount)
    {
        charge += amount;
        charge = Mathf.Max(Mathf.Min(charge, maxCharge), 0f);

        Status.transform.localScale = Vector3.one * (1.5f * charge);
        var newLevel = charge == 0f ? 0: (charge > 0.5 ? 2 : 1) ;
        if (newLevel != level)
        {
            spriteRenderer.sprite = sprites[newLevel];
        }

        level = newLevel;
    }

    private void SetActivateEnd()
    {
        Status.transform.localScale = Vector3.one*0f;
        radiusUI.Hide();
        isActivateEnd = true;
    }
    
    void FixedUpdate()
    {

        switch (stateMachine.CurrentState)
        {
            case EnvStateEnum.ZeroCharge:
                if (charge > 0)
                {
                    stateMachine.SetNextState(EnvStateEnum.Charging);
                    dischargeCountDown = maxDischargeCountDown;
                }
                break;
            case EnvStateEnum.Charging:
                if (MathUtils.IsFloatEqual(1f, charge) && isAddCharge)
                {
                    stateMachine.SetNextState(EnvStateEnum.Activataed);
                }
                else if (dischargeCountDown <= 0f)
                {
                    stateMachine.SetNextState(EnvStateEnum.Discharging);
                }
                break;
            case EnvStateEnum.Discharging:
                if (charge == 0f)
                {
                    stateMachine.SetNextState(EnvStateEnum.ZeroCharge);
                }
                if (isAddCharge)
                {
                    stateMachine.SetNextState(EnvStateEnum.Charging);
                    dischargeCountDown = maxDischargeCountDown;
                }

                break;
            case EnvStateEnum.Activataed:
                if (isActivateEnd)
                {
                    stateMachine.SetNextState(EnvStateEnum.ZeroCharge);
                    level = 0;
                    ChangeCharge(-float.NegativeInfinity);
                    isActivateEnd = false;
                }
                break;
        }
        
        if (isAddCharge)
        {
            ChangeCharge(0.5f); // 1/4 charge
        }
        
        stateMachine.ChangeState();
        
        switch (stateMachine.CurrentState)
        {
            case EnvStateEnum.ZeroCharge:
                break;
            case EnvStateEnum.Charging:
                dischargeCountDown -= Time.fixedDeltaTime;
                break;
            case EnvStateEnum.Discharging:
                ChangeCharge( -dischargeRate*Time.fixedDeltaTime);
                break;
            case EnvStateEnum.Activataed:
                if (stateMachine.PreviousState != EnvStateEnum.Activataed)
                {
                    ActivateBlackHole();
                    
                }
                break;
        }

        isAddCharge = false;
    }

    private void ActivateBlackHole()
    {
        radiusUI.Show(1f);
        Invoke("ActivateBlackHoleJing2", 1f);
    }

    private void ActivateBlackHoleJing2()
    {
        
        GameObject.Instantiate(blackHolePrefab, transform.position, quaternion.Euler(Vector3.zero));
        Invoke("SetActivateEnd", 10f);
    }
    

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
            Handles.Label(transform.position, String.Format("Level {0} Charge {1} State {2}", level, charge, stateMachine.CurrentState));
    }
}