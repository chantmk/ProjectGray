using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] public float maxHealth = 100.0f;
    [SerializeField] public float currentHealth { get; private set; }

    private const float depleteHealth = 0.0f;

    public float damage;
    public float armor;
    public bool isDead;
    
    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (!isDead)
        {
            damage -= this.armor;

            if (damage < 0.0f) damage = 0.0f;

            currentHealth -= damage;
            Debug.Log(transform.name + " -" + damage + " Health");
            if (currentHealth <= depleteHealth)
            {
                Die();
            }
        }
        
    }

    public void Heal(float healValue)
    {
        if (!isDead)
        {
            if (healValue < 0.0f)
            {
                healValue = 0.0f;
            }

            currentHealth += healValue;
            Debug.Log(transform.name + " +" + healValue + " Health");
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        }
        
    }

    public virtual void Die()
    {
        isDead = true;
        Debug.Log(transform.name + " Died");
        Destroy(gameObject);
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetHealthPercentage()
    {
        return currentHealth / maxHealth;
    }


    // Start is called before the first frame update
    protected virtual void Start()
    {
        isDead = false;
        currentHealth = maxHealth;
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
}
