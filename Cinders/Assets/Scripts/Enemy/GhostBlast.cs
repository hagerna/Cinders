using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBlast : MonoBehaviour
{
    [SerializeField] AnimationCurve SizeCurve;
    private bool launch = false;
    private bool turned = false;
    private float projectileSpeed, damageTransferred;
    [SerializeField] Material TurnedColor;
    [SerializeField] GameObject EnemyTrail, PlayerTrail, BurstEffect;
    // Start is called before the first frame update

    public void Fire(float speed = 2)
    {
        projectileSpeed = speed;
        launch = true;
        StartCoroutine(AutoDestroy());
    }

    public void Charge(float chargeTime)
    {
        StartCoroutine(Expand(chargeTime));
    }

    IEnumerator Expand(float chargeTime)
    {
        float time = 0;
        while (time < chargeTime)
        {
            float size = SizeCurve.Evaluate(time / chargeTime);
            transform.localScale = new Vector3(size, size, size);
            time += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

    }

    protected void FixedUpdate()
    {
        if (launch)
        {
            transform.LookAt(Vector3.up);
            transform.position += projectileSpeed * Time.deltaTime * transform.forward;
        }
    }

    public void HitByPlayer(float damage)
    {
        turned = true;
        gameObject.GetComponent<Renderer>().material = TurnedColor;
        EnemyTrail.SetActive(false);
        PlayerTrail.SetActive(true);
        damageTransferred = damage;
    }

    IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(12f);
        Destroy(gameObject);
        Debug.Log("Destroyed");
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
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Scenery"))
        {
            Destroy(gameObject);
        }
        if (turned && collision.collider.CompareTag("Enemy"))
        {
            collision.collider.GetComponent<Enemy>().Hurt(damageTransferred);
        }
    }
}
