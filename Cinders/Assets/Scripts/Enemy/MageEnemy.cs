using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageEnemy : Enemy
{
    [SerializeField] float AttackDistance, AttackSpeed;
    private bool IsAttacking;
    [SerializeField] GameObject Projectile;
    GameObject CurrentProjectile;
    [SerializeField] Transform ProjectileStart;

    override protected void EnemyDeath()
    {
        Destroy(CurrentProjectile);
        base.EnemyDeath();
    }

    protected new void FixedUpdate()
    {
        if (!IsAttacking && Vector3.Distance(Vector3.up, transform.position) <= AttackDistance)
        {
            transform.LookAt(Vector3.up);
            IsAttacking = true;
            StartCoroutine(Attack());
        }
        else if (!IsAttacking && CanMove)
        {
            Move();
        }
    }

    IEnumerator Attack()
    {
        CurrentProjectile = Instantiate(Projectile, ProjectileStart.position, transform.rotation);
        GhostBlast blast = CurrentProjectile.GetComponent<GhostBlast>();
        if (blast != null)
        {
            blast.Charge(AttackSpeed);
        }
        yield return new WaitForSeconds(AttackSpeed);
        if (CurrentProjectile != null)
        {
            blast.Fire();
        }
        CurrentProjectile = null;
        yield return new WaitForSeconds(Random.Range(0f, 2f));
        IsAttacking = false;
    }
}
