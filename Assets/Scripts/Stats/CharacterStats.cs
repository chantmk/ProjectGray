using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public abstract class CharacterStats : MonoBehaviour
{
    [Header("Character base status")]
    public int BaseMaxHealth;
    public int MaxHealth;
    public int CurrentHealth { get; protected set; }
    public int Armor;
    public StatusEnum Status = StatusEnum.Mortal;
    [SerializeField]
    protected GameObject healthBar;
    protected Image healthBarImage;

    public int depleteHealth = 0;
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
            TakeDamage(1);
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
    
    public virtual void TakeDamage(int damage)
    {
        if (Status == StatusEnum.Mortal)
        {
            damage -= this.Armor;

            if (damage < 0) damage = 0;

            CurrentHealth -= damage;
            HandleHealth();
        }

    }

    public void Heal(int healValue)
    {
        if (Status != StatusEnum.Dead)
        {
            if (healValue < 0)
            {
                healValue = 0;
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
        return (float)CurrentHealth / (float)MaxHealth;
    }

    public void setMaxHealth(int health)
    {
        MaxHealth = health;
    }
}
