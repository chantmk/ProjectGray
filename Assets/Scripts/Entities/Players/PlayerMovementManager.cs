using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;


public class PlayerMovementManager : MonoBehaviour
{
    public CameraShake CameraShake;

    public float blueDebuffSlowCoeff = 0.5f;
    
    [SerializeField] private Rigidbody2D playerRigidbody;

    [SerializeField] private float moveSpeed = 2.0f;

    private PlayerStats playerStats;

    [SerializeField] private float rechargeStamina => (playerStats.RechargeStamina);

    private float rawInputX;
    private float rawInputY;
    private bool inputRoll;

    private Animator animator;
    public float AnimationSpeed = 1f;
    private float animationSpeed = 0.2f;
    private StateMachine<MovementEnum> stateMachine;

    [SerializeField] private bool isExhault = false;
    [SerializeField] private float stamina = 100f;
    [SerializeField] private int inputX, inputY;
    [SerializeField] private Vector2 movement;
    [SerializeField] private GameObject staminaBar;
    private Image image;
    
    public float RollStaminaCost = 30f;
    public float MaxRollDuration = 0.5f;
    public float rollDuration;
    private Vector2 rollDirection;
    public float RollSpeed;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        if (staminaBar == null)
            staminaBar = GameObject.FindGameObjectWithTag("StaminaBar");

        animator = GetComponent<Animator>();
        animator.SetFloat(AnimatorParams.AnimSpeed, animationSpeed);
        
        stateMachine = new StateMachine<MovementEnum>(MovementEnum.Idle);
        image = staminaBar.GetComponent<Image>();
        playerStats = GetComponent<PlayerStats>();
        
        CameraShake = GameObject.Find("CameraHolder").GetComponentInChildren<CameraShake>();
    }

    void Update()
    {
        if (PauseManager.GamePaused)
        {
            return;
        }
        rawInputX = Input.GetAxisRaw("Horizontal");
        rawInputY = Input.GetAxisRaw("Vertical");
        inputRoll = Input.GetKeyDown(KeyCode.Space);

        inputX = rawInputX == 0 ? 0 : (int)Mathf.Sign(rawInputX);
        inputY = rawInputY == 0 ? 0 : (int)Mathf.Sign(rawInputY);
        var isBlueDebuff = playerStats.DebuffColor == ColorEnum.Blue;
        var isStartRolling = inputRoll && stamina > 0 && !isExhault;

        // Block Roll while on blue
        if (isStartRolling && isBlueDebuff)
        {
            StartCoroutine(CameraShake.Shake(0.1f, 0.1f));
            isStartRolling = false;
        }
        
        var isInputMoving = (inputX != 0 || inputY != 0);
        // Update State
        switch (stateMachine.CurrentState)
        {
            case MovementEnum.Idle:
                if (isStartRolling)
                {
                    stateMachine.SetNextState(MovementEnum.Roll);
                }
                if (isInputMoving)
                    stateMachine.SetNextState(MovementEnum.Move);
                    
                break;
            case MovementEnum.Move:
                if (isStartRolling)
                    stateMachine.SetNextState(MovementEnum.Roll);
                if (!isInputMoving)
                    stateMachine.SetNextState(MovementEnum.Idle);
                break;
            case MovementEnum.Roll:
                if (rollDuration > MaxRollDuration)
                    if (isInputMoving)
                        stateMachine.SetNextState(MovementEnum.Move);
                    else
                        stateMachine.SetNextState(MovementEnum.Idle);
                break;

        }
        stateMachine.ChangeState();
        // print(stateMachine.CurrentState);

        if (stateMachine.CurrentState != MovementEnum.Roll && stateMachine.PreviousState == MovementEnum.Roll)
        {
            playerStats.ImmortalBuffCount -= 1;
        }
        
        if (stateMachine.CurrentState == MovementEnum.Roll && stateMachine.PreviousState != MovementEnum.Roll)
        {
            playerStats.ImmortalBuffCount += 1;
        }
        
        switch (stateMachine.CurrentState)
        {
            case MovementEnum.Move:
                // coefficient = Mathf.Pow(expValue, tileData[0]);
                movement = new Vector2(inputX, inputY);
                movement = movement.normalized * moveSpeed;
                if (isBlueDebuff)
                {
                    movement *= blueDebuffSlowCoeff;
                }
                break;
            
            case MovementEnum.Roll:
                if (stateMachine.PreviousState != MovementEnum.Roll)
                {

                    stamina -= RollStaminaCost;
                    if (stamina <= 0.0f)
                    {
                        stamina = 0.0f;
                        isExhault = true;
                    }

                    rollDuration = 0;
                    rollDirection = new Vector2(inputX, inputY).normalized;
                }
                else
                {
                    rollDuration += Time.deltaTime;
                }

                movement = rollDirection * RollSpeed;

                break;
            case MovementEnum.Idle:
                movement = Vector2.zero;
                break;
            default:
                break;
        }
        
        if (stateMachine.CurrentState != MovementEnum.Roll)
        {
            stamina += 1f;
            if (stamina > 100.0f)
            {
                stamina = 100.0f;
            }
            else if(isExhault && stamina > rechargeStamina)
            {
                isExhault = false;
            }
            
        }
        
        image.fillAmount = stamina / 100;
        // Update Animation
        animator.SetInteger(AnimatorParams.Movement, (int)stateMachine.CurrentState);

        animator.SetFloat(AnimatorParams.Horizontal, movement.x);
        animator.SetFloat(AnimatorParams.Vertical, movement.y);

    }
    

    private void FixedUpdate()
    {
        playerRigidbody.velocity = movement;
    }
}
