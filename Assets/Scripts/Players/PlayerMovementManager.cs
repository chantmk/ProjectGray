using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class PlayerMovementManager : MonoBehaviour
{

    [SerializeField] private Rigidbody2D playerRigidbody;

    [SerializeField] private float normalSpeed = 2.0f;

    [SerializeField] private float runningSpeed = 5.0f;

    [SerializeField] private float rechargeStamina = 20.0f;

    [SerializeField] private float rawInputX;

    [SerializeField] private float rawInputY;

    [SerializeField] private bool rawInputShift;

    [SerializeField] private static float xySpeed = 2.0f;
    [SerializeField] private float[] tileData;

    private Animator animator;
    public float AnimationSpeed = 1f;
    private float animationSpeed = 0.2f;
    public enum States { Idle, Movement };
    private StateMachine<States> stateMachine;
    
    [SerializeField] private float expValue = 0.8f;
    [SerializeField] private bool isExhault = false;
    [SerializeField] private float stamina = 100f;
    [SerializeField] private int inputX, inputY;
    [SerializeField] private Vector2 movement;
    [SerializeField] private GameObject tilemapObj;
    [SerializeField] private GameObject staminaBar;
    [SerializeField] private float coefficient;
    private Image image;
    private RandomTile randomTile;


    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        if (tilemapObj == null)
            tilemapObj = GameObject.FindGameObjectWithTag("Tilemap");
        if (staminaBar == null)
            staminaBar = GameObject.FindGameObjectWithTag("StaminaBar");

        animator = GetComponent<Animator>();
        animator.SetFloat(AnimatorParams.AnimSpeed, animationSpeed);
        
        stateMachine = new StateMachine<States>(States.Idle);
        
        randomTile = tilemapObj.GetComponent<RandomTile>();
        image = staminaBar.GetComponent<Image>();
    }

    void Update()
    {
        rawInputX = Input.GetAxisRaw("Horizontal");
        rawInputY = Input.GetAxisRaw("Vertical");
        rawInputShift = Input.GetKey(KeyCode.LeftShift);
        tileData = randomTile.getCurrentTileSpeed();

        inputX = rawInputX == 0 ? 0 : (int)Mathf.Sign(rawInputX);
        inputY = rawInputY == 0 ? 0 : (int)Mathf.Sign(rawInputY);
        image.fillAmount = stamina / 100;
        coefficient = Mathf.Pow(expValue, tileData[0]);
        movement = new Vector2(inputX, inputY);
        movement = movement.normalized * (xySpeed * coefficient);
        
        // Update State
        if (movement.sqrMagnitude < MathUtils.Epsilon)
        {
            print("idle");
            stateMachine.SetNextState(States.Idle);
        }
        else
        {
            
            stateMachine.SetNextState(States.Movement);
        }
        stateMachine.ChangeState();
        
        // Update Animation
        animator.SetInteger(AnimatorParams.State, (int)stateMachine.CurrentState);

        animator.SetFloat(AnimatorParams.Horizontal, movement.x);
        animator.SetFloat(AnimatorParams.Vertical, movement.y);
        
        if (rawInputShift && stamina > 0 && !isExhault)
        {
            xySpeed = runningSpeed;
            stamina -= 1.0f;
            if(stamina <= 0.0f)
            {
                isExhault = true;
            }
        }
        else
        {
            xySpeed = normalSpeed;
            stamina += 0.08f;
            if (stamina > 100.0f)
            {
                stamina = 100.0f;
            }
            else if(isExhault && stamina > rechargeStamina)
            {
                isExhault = false;
            }
        }
        if (tileData[2] != 0f)stamina -= tileData[2] * 0.01f + 0.1f;
        image.fillAmount = stamina / 100;
        coefficient = Mathf.Pow(expValue, tileData[0]);
        movement = new Vector2(inputX, inputY);
        //Debug.Log(coefficient);
        movement = movement.normalized * (xySpeed * coefficient);
    }

    private void FixedUpdate()
    {
        playerRigidbody.velocity = movement;
    }
}
