using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class CharacterStats : MonoBehaviour
{
    [Header("Character base status")]
    public float MaxHealth = 100.0f;
    public float CurrentHealth { get; protected set; }
    public float Armor;
    public StatusEnum Status = StatusEnum.Mortal;

    protected const float depleteHealth = 0.01f;

    private readonly List<StatBuff> statBuffs;
    private readonly List<MovementBuff> movementBuffs;

    public CharacterStats()
    {
        statBuffs = new List<StatBuff>();
        movementBuffs = new List<MovementBuff>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Status = StatusEnum.Mortal;
        CurrentHealth = MaxHealth;

        

        //TEST STAT
        //MaxHealthIncreaseBuff buff = new MaxHealthIncreaseBuff();
        statBuffs.Add(new MaxHealthIncreaseBuff());
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // TODO: remove (For debugging)
        if (Input.GetKeyDown(KeyCode.L))
        {
            TakeDamage(5.0f);
        }

        ApplyStatBuff();

    }

    void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(float damage)
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
        return CurrentHealth / MaxHealth;
    }

    public void setMaxHealth(float health)
    {
        MaxHealth = health;
    }

    public void AddStatBuff(StatBuff buff)
    {
        statBuffs.Add(buff);
        ApplyStatBuff();
    }

    public void RemoveStatBuff(StatBuff buff)
    {
        statBuffs.Remove(buff);
    }

    private void ApplyStatBuff()
    {
        foreach (var statBuff in statBuffs)
        {
            statBuff.Apply(this);
        }
    }

    private void UpdateStatBuff()
    {
        foreach (var statBuff in statBuffs)
        {
           // statBuff.Update(this);
        }
    }
}
