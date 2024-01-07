using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    protected float health;
    protected bool canMove;
    protected Rigidbody rb;
    [SerializeField] protected float moveSpeed = 2f;
    [SerializeField] protected string type = "Basic";
    [SerializeField] protected bool isElite;
    [SerializeField] protected GameObject deathEffect;

    protected void Start()
    {
        GameManager manager = FindObjectOfType<GameManager>();
        if (manager)
        {
            health = manager.GetEnemyHealth(type, isElite);
        }
        rb = GetComponent<Rigidbody>();
        canMove = true;
    }
        

    protected void FixedUpdate()
    {
        if (canMove)
            Move();
    }

    protected void Move()
    {
        transform.LookAt(Vector3.up);
        rb.velocity = moveSpeed * transform.forward;
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

    virtual protected void EnemyDeath()
    {
        Instantiate(deathEffect, transform);
        Destroy(gameObject);
    }

    virtual protected void EnemyInjured()
    {
        StartCoroutine(Knockback());
    }

    IEnumerator Knockback()
    {
        canMove = false;
        GetComponent<Rigidbody>().AddForce(transform.forward * -10, ForceMode.Impulse);
        yield return new WaitForSeconds(0.5f);
        canMove = true;
    }



    protected void ReachedCampfire()
    {
        Instantiate(deathEffect, transform);
        EnemyDeath();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Campfire"))
        {
            Campfire fire = other.GetComponent<Campfire>();
            if (fire)
            {
                fire.FireReached();
            }
            ReachedCampfire();
        }
    }
}
