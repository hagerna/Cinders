using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firebolt : MonoBehaviour
{
    private GameObject target;
    private Vector3 start;

    [SerializeField] private float Duration;
    private float Damage, Speed;
    private bool Thrown = false;
    [SerializeField] AnimationCurve Curve;
    //public ParticleSystem.MinMaxCurve Curve = new ParticleSystem.MinMaxCurve(1, new AnimationCurve(), new AnimationCurve());

    public void SetTarget(GameObject Target, float pullSpeed)
    {
        start = transform.position;
        target = Target;
        Speed = pullSpeed;
        StartCoroutine(Pull());
    }

    IEnumerator Pull()
    {
        float time = 0;
        while (!Thrown && time <= Duration)
        {
            transform.position = Vector3.Lerp(start, target.transform.position, Curve.Evaluate(time/Duration));
            time += Time.deltaTime * Speed;
            yield return new WaitForEndOfFrame();
        }
        if (!Thrown)
        {
            transform.SetParent(target.transform);
        }
    }

    public void Throw(Vector3 throwVelocity, float hitDamage)
    {
        Thrown = true;
        Rigidbody rb = GetComponent<Rigidbody>();
        transform.SetParent(null);
        rb.useGravity = true;
        rb.velocity = throwVelocity;
        Damage = hitDamage;

    }

    private void HitEnemy(GameObject target)
    {
        Enemy enemy = target.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.Hurt(Damage);
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
