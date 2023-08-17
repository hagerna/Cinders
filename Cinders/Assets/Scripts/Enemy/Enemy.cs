using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    protected float Health, MoveSpeed;
    protected bool CanMove;
    [SerializeField] protected string Type = "Basic";
    [SerializeField] protected bool IsElite;

    protected void Start()
    {
        GameManager gm = FindObjectOfType<GameManager>();
        if (gm == null)
        {
            Health = gm.GetEnemyHealth(Type, IsElite);
            //MoveSpeed = gm.GetEnemySpeed(Type, IsElite);
            MoveSpeed = 2;
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

    protected void EnemyDeath()
    {
        Destroy(gameObject);
    }

    protected void EnemyInjured()
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
