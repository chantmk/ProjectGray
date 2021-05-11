using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public abstract class CharacterStats : MonoBehaviour
{
    [Header("Character base status")]
    public float BaseMaxHealth;
    public float MaxHealth;
    public float CurrentHealth { get; protected set; }
    public float Armor;
    public StatusEnum Status = StatusEnum.Mortal;
    [SerializeField]
    protected GameObject healthBar;
    protected Image healthBarImage;

    protected const float depleteHealth = 0.01f;

    //private readonly List<MovementBuff> movementBuffs;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Status = StatusEnum.Mortal;
        MaxHealth = BaseMaxHealth;
        CurrentHealth = MaxHealth;
        GetHealthBarImage();
    }


    // Update is called once per frame
    protected virtual void Update()
    {
        // TODO: remove (For debugging)
        if (Input.GetKeyDown(KeyCode.L))
        {
            TakeDamage(5.0f);
        }
    }

    void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    protected abstract void GetHealthBarImage();
    protected virtual void HandleHealthBar()
    {
        healthBarImage.fillAmount = GetHealthPercentage();
    }
    
    public virtual void TakeDamage(float damage)
    {
        if (Status == StatusEnum.Mortal)
        {
            damage -= this.Armor;

            if (damage < GrayConstants.EPSILON) damage = 0.0f;

            CurrentHealth -= damage;
            //Debug.Log(transform.name + " -" + damage + " Health left: " + CurrentHealth);
            HandleHealth();
        }

    }

    public void Heal(float healValue)
    {
        if (Status != StatusEnum.Dead)
        {
            if (healValue < GrayConstants.EPSILON)
            {
                healValue = 0.0f;
            }

            CurrentHealth += healValue;
            //Debug.Log(transform.name + " +" + healValue + " Health");
            if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
        }

    }

    public virtual void HandleHealth()
    {
        HandleHealthBar();
        if (CurrentHealth <= depleteHealth)
        {
            HealthRunOut();
        }
    }

    public virtual void HealthRunOut()
    {
        Status = StatusEnum.Dead;
        Debug.Log(transform.name + " Died");
        Die();
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    public float GetCurrentHealth()
    {
        return CurrentHealth;
    }

    public float GetHealthPercentage()
    {
        return CurrentHealth / MaxHealth;
    }

    public void setMaxHealth(float health)
    {
        MaxHealth = health;
    }
}
