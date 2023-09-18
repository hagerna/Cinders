using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    [SerializeField] GameObject Flame;
    private int TorchHits, MaxHits;
    private float TorchDamage;

    // Start is called before the first frame update
    void Start()
    {
        TorchHits = 0;
        Flame.SetActive(false);
    }

    public void SetMaxHits(int max)
    {
        MaxHits = max;
    }

    public void SetTorchDamage(float dmg)
    {
        TorchDamage = dmg;
    }
    

    private void Relight()
    {
        Flame.SetActive(true);
        TorchHits = MaxHits;
    }

    private void HitEnemy(GameObject target)
    {
        if (TorchHits <= 0)
        {
            return;
        }
        Enemy enemy = target.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.Hurt(TorchDamage);
        }
        HandleHitCount();
    }

    private void HitProjectile(GameObject target)
    {
        GhostBlast blast = target.GetComponent<GhostBlast>();
        if (blast != null)
        {
            blast.HitByPlayer(TorchDamage);
        }
        TorchHits = 0;
    }

    private void HandleHitCount()
    { 
        TorchHits--;
        if (TorchHits <= 0)
        {
            Flame.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Campfire"))
        {
            Relight();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (TorchHits > 0 && collision.collider.CompareTag("Enemy"))
        {
            HitEnemy(collision.gameObject);
        }
        else if (TorchHits > 0 && collision.collider.CompareTag("Projectile"))
        {
            HitProjectile(collision.gameObject);
        }
    }
}
