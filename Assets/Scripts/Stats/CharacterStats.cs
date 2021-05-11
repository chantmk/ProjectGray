using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class CharacterStats : MonoBehaviour
{
    [Header("Character base status")]
    public int BaseMaxHealth;
    public int MaxHealth;
    public int CurrentHealth { get; protected set; }
    public int Armor;
    public StatusEnum Status = StatusEnum.Mortal;

    public int depleteHealth = 0;
    //private readonly List<MovementBuff> movementBuffs;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Status = StatusEnum.Mortal;
        CurrentHealth = MaxHealth;
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

    public void TakeDamage(int damage)
    {
        if (Status == StatusEnum.Mortal)
        {
            damage -= this.Armor;

            if (damage < 0) damage = 0;

            CurrentHealth -= damage;
            //Debug.Log(transform.name + " -" + damage + " Health left: " + CurrentHealth);
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
        if (CurrentHealth <= depleteHealth)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Status = StatusEnum.Dead;
        Debug.Log(transform.name + " Died");
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
