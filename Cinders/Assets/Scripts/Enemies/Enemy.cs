using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] protected float health, maxHealth, speed;

    protected void Start()
    {
        health = maxHealth;
    }
        

    protected void FixedUpdate()
    {
        Move();
    }

    protected void Move()
    {
        transform.LookAt(Vector3.up);
        transform.position += speed * Time.deltaTime * transform.forward;
    }

    public void Hurt(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            EnemyDeath();
        }
        else
        {
            EnemyInjured();
        }
    }

    protected void EnemyDeath()
    {
        Destroy(gameObject);
    }

    protected void EnemyInjured()
    {
        Destroy(gameObject);
    }

    protected void ReachedCampfire()
    {
        EnemyDeath();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Campfire"))
        {
            ReachedCampfire();
        }
    }
}
