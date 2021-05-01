using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class PlayerMovementManager : MonoBehaviour
{

    [SerializeField] private Rigidbody2D playerRigidbody;

    [SerializeField] private float moveSpeed = 2.0f;

    [SerializeField] private float rechargeStamina = 20.0f;

    private float rawInputX;
    private float rawInputY;
    private bool inputRoll;

    private Animator animator;
    public float AnimationSpeed = 1f;
    private float animationSpeed = 0.2f;
    public enum States { Idle, Movement, Rolling };
    private StateMachine<States> stateMachine;

    [SerializeField] private float expValue = 0.8f;
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
        
        stateMachine = new StateMachine<States>(States.Idle);
        image = staminaBar.GetComponent<Image>();
    }

    void Update()
    {
        rawInputX = Input.GetAxisRaw("Horizontal");
        rawInputY = Input.GetAxisRaw("Vertical");
        inputRoll = Input.GetKeyDown(KeyCode.Space);

        inputX = rawInputX == 0 ? 0 : (int)Mathf.Sign(rawInputX);
        inputY = rawInputY == 0 ? 0 : (int)Mathf.Sign(rawInputY);

        var isStartRolling = inputRoll && stamina > 0 && !isExhault;
        var isInputMoving = (inputX != 0 || inputY != 0);
        // Update State
        switch (stateMachine.CurrentState)
        {
            case States.Idle:
                if (isStartRolling)
                    stateMachine.SetNextState(States.Rolling);
                if (isInputMoving)
                    stateMachine.SetNextState(States.Movement);
                    
                break;
            case States.Movement:
                if (isStartRolling)
                    stateMachine.SetNextState(States.Rolling);
                if (!isInputMoving)
                    stateMachine.SetNextState(States.Idle);
                break;
            case States.Rolling:
                if (rollDuration > MaxRollDuration)
                    if (isInputMoving)
                        stateMachine.SetNextState(States.Movement);
                    else
                        stateMachine.SetNextState(States.Idle);
                break;

        }
        stateMachine.ChangeState();
        // print(stateMachine.CurrentState);
        
        switch (stateMachine.CurrentState)
        {
            case States.Movement:
                
                // coefficient = Mathf.Pow(expValue, tileData[0]);
                movement = new Vector2(inputX, inputY);
                //Debug.Log(coefficient);
                movement = movement.normalized * moveSpeed;
                break;
            
            case States.Rolling:
                if (stateMachine.PreviousState != States.Rolling)
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
            case States.Idle:
                movement = Vector2.zero;
                break;
            default:
                break;
        }
        
        if (stateMachine.CurrentState != States.Rolling)
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
        animator.SetInteger(AnimatorParams.State, (int)stateMachine.CurrentState);

        animator.SetFloat(AnimatorParams.Horizontal, movement.x);
        animator.SetFloat(AnimatorParams.Vertical, movement.y);

    }
    

    private void FixedUpdate()
    {
        playerRigidbody.velocity = movement;
    }
}
