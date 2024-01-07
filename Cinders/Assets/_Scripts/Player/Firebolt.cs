using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firebolt : MonoBehaviour
{
    protected GameObject target;
    protected Vector3 startPosition;

    protected float duration = 2;
    protected float damage, speed;
    protected bool thrown = false;
    [SerializeField] AnimationCurve pullCurve;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetTarget(GameObject Target, float pullSpeed)
    {
        startPosition = transform.position;
        this.target = Target;
        speed = pullSpeed;
        StartCoroutine(Pull());
    }

    IEnumerator Pull()
    {
        float time = 0;
        while (!thrown && time <= duration)
        {
            transform.position = Vector3.Lerp(startPosition, target.transform.position, pullCurve.Evaluate(time/duration));
            time += Time.deltaTime * speed;
            yield return new WaitForEndOfFrame();
        }
        if (!thrown)
        {
            transform.SetParent(target.transform);
            rb.isKinematic = true;
        }
    }

    virtual public void Throw(Vector3 throwVelocity, float hitDamage)
    {
        thrown = true;
        transform.SetParent(null);
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.velocity = throwVelocity;
        damage = hitDamage;

    }

    virtual protected void HitEnemy(GameObject target)
    {
        Enemy enemy = target.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.Hurt(damage);
        }
        Destroy(gameObject);
    }

    virtual protected void HitProjectile(GameObject target)
    {
        GhostBlast blast = target.GetComponent<GhostBlast>();
        if (blast != null)
        {
            blast.HitByPlayer(damage);
        }
        Destroy(gameObject);
    }

    virtual protected void HitObject()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.collider.tag)
        {
            case "Enemy":
                HitEnemy(collision.gameObject);
                break;
            case "Projectile":
                HitProjectile(collision.gameObject);
                break;
            case "Scenery":
                HitObject();
                break;
            default:
                break;
        }
    }
}
