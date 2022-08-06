using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour
{
    public float speed;
    public int damage;
    public int maxHealth;

    protected float timeToTakeDamage = 2.5f;
    protected bool canTakeDamage;
    protected int currentHealth;

    public int CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }

    public virtual void Start()
    {
        currentHealth = maxHealth;
    }

    public virtual void Update()
    {
        DamageCooldown();
    }

    public void TakeDamage(int damage)
    {
        if(canTakeDamage)
        {
            currentHealth -= damage;
            canTakeDamage = false;
            timeToTakeDamage = 1f;
        }
    }

    private void DamageCooldown()
    {
        timeToTakeDamage -= Time.deltaTime;

        if (timeToTakeDamage <= 0f) canTakeDamage = true;
    }
}