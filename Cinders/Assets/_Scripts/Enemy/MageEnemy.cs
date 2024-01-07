using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageEnemy : Enemy
{
    [SerializeField] float attackDistance, attackSpeed;
    private bool isAttacking;
    [SerializeField] GameObject projectile;
    GameObject currentProjectile;
    [SerializeField] Transform projectileStart;

    override protected void EnemyDeath()
    {
        Destroy(currentProjectile);
        base.EnemyDeath();
    }

    protected new void FixedUpdate()
    {
        if (!isAttacking && Vector3.Distance(Vector3.up, transform.position) <= attackDistance)
        {
            transform.LookAt(Vector3.up);
            rb.velocity = Vector3.zero;
            isAttacking = true;
            StartCoroutine(Attack());
        }
        else if (!isAttacking && canMove)
        {
            Move();
        }
    }

    IEnumerator Attack()
    {
        currentProjectile = Instantiate(projectile, projectileStart.position, transform.rotation);
        GhostBlast blast = currentProjectile.GetComponent<GhostBlast>();
        if (blast != null)
        {
            blast.Charge(attackSpeed);
        }
        yield return new WaitForSeconds(attackSpeed);
        if (currentProjectile != null)
        {
            blast.Fire();
        }
        currentProjectile = null;
        yield return new WaitForSeconds(Random.Range(0f, 2f));
        isAttacking = false;
    }
}
