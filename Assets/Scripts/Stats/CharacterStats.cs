using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] public float maxHealth;
    [SerializeField] public float currentHealth { get; private set; }

    public Stats damage = 0.0f;
    public Stats armor = 0.0f;
    
    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        damage -= this.armor.GetValue();

        damage = Mathf.Clamp(damage, 0, float.MaxValue);

        currentHealth -= damage;
        Debug.Log(transform.name + " -" + damage + " Health");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Debug.Log(transform.name + " Died")
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            TakeDamage(5.0f);
        }
    }
}
