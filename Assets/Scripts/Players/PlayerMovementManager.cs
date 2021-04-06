using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementManager : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRigidbody;

    [SerializeField] private float normalSpeed = 2.0f;
    [SerializeField] private float runningSpeed = 5.0f;
    [SerializeField] private float rechargeStamina = 20.0f;
    [SerializeField] private float expValue = 0.8f;
    [SerializeField] private float rawInputX;
    [SerializeField] private float rawInputY;
    [SerializeField] private bool rawInputShift;
    [SerializeField] private static float xySpeed = 2.0f;
    [SerializeField] private float tileData;
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
        
        randomTile = tilemapObj.GetComponent<RandomTile>();
        image = staminaBar.GetComponent<Image>();
    }

    void Update()
    {
        rawInputX = Input.GetAxisRaw("Horizontal");
        rawInputY = Input.GetAxisRaw("Vertical");
        rawInputShift = Input.GetKey(KeyCode.LeftShift);
        tileData = randomTile.getCurrentTileData();

        inputX = rawInputX == 0 ? 0 : (int)Mathf.Sign(rawInputX);
        inputY = rawInputY == 0 ? 0 : (int)Mathf.Sign(rawInputY);
        
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
            stamina += 0.1f;
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
        coefficient = Mathf.Pow(expValue, tileData);
        movement = new Vector2(inputX, inputY);
        //Debug.Log(coefficient);
        movement = movement.normalized * (xySpeed * coefficient);
    }

    private void FixedUpdate()
    {
        playerRigidbody.velocity = movement;
    }

    /*public void setSpeed(float speed)
    {
        
        *//*xSpeed = xySpeed / (1f + speed);
        ySpeed = xySpeed / (1f + speed);*//*
        //Debug.Log(xSpeed);
    }*/
}
