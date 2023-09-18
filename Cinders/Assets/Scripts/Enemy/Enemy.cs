using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    protected float Health;
    [SerializeField] protected float MoveSpeed = 2f;
    protected bool CanMove;
    [SerializeField] protected string Type = "Basic";
    [SerializeField] protected bool IsElite;
    [SerializeField] protected GameObject DeathEffect;

    protected void Start()
    {
        Instantiate(DeathEffect, Vector3.up, Quaternion.identity);
        GameManager gm = FindObjectOfType<GameManager>();
        if (gm != null)
        {
            Health = gm.GetEnemyHealth(Type, IsElite);
        }
        CanMove = true;
    }
        

    protected void FixedUpdate()
    {
        if (CanMove)
            Move();
    }

    protected void Move()
    {
        transform.LookAt(Vector3.up);
        transform.position += MoveSpeed * Time.deltaTime * transform.forward;
    }

    public void Hurt(float damage)
    {
        Health -= damage;
        if (Health <= 0)
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
        Instantiate(DeathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    virtual protected void EnemyInjured()
    {
        StartCoroutine(Knockback());
    }

    IEnumerator Knockback()
    {
        CanMove = false;
        GetComponent<Rigidbody>().AddForce(transform.forward * -10, ForceMode.Impulse);
        yield return new WaitForSeconds(0.5f);
        CanMove = true;
    }



    protected void ReachedCampfire()
    {
        Instantiate(DeathEffect, transform);
        EnemyDeath();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Campfire"))
        {
            Campfire fire = other.GetComponent<Campfire>();
            if (fire != null)
            {
                fire.FireReached();
            }
            ReachedCampfire();
        }
    }
}
