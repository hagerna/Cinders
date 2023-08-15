using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firebolt : MonoBehaviour
{
    private GameObject target;
    private Vector3 start;
    private float duration, speed, damage;
    private bool thrown = false;
    [SerializeField] AnimationCurve Curve;
    //public ParticleSystem.MinMaxCurve Curve = new ParticleSystem.MinMaxCurve(1, new AnimationCurve(), new AnimationCurve());

    public void SetTarget(GameObject Target)
    { 
        start = transform.position;
        target = Target;
        StartCoroutine(Pull());
    }

    IEnumerator Pull()
    {
        float time = 0;
        while (!thrown && time <= duration)
        {
            transform.position = Vector3.Lerp(start, target.transform.position, Curve.Evaluate(time/duration));
            time += Time.deltaTime * speed;
            yield return new WaitForEndOfFrame();
        }
        if (!thrown)
        {
            transform.SetParent(target.transform);
        }
    }

    public void Throw(Vector3 throwVelocity, float hitDamage)
    {
        thrown = true;
        Rigidbody rb = GetComponent<Rigidbody>();
        transform.SetParent(null);
        rb.useGravity = true;
        rb.velocity = throwVelocity;
        damage = hitDamage;

    }

    private void HitEnemy(GameObject target)
    {
        Enemy enemy = target.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.Hurt(damage);
        }
        Destroy(gameObject);
    }

    private void HitObject()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            HitEnemy(collision.gameObject);
        }
        if (collision.collider.CompareTag("Campfire"))
        {
            return;
        }
        else
        {
            HitObject();
        }
    }
}
