using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    [SerializeField] GameObject flame;
    private int torchHits, maxHits;
    private float torchDamage;
    public bool useable;

    // Start is called before the first frame update
    void Start()
    {
        torchHits = 0;
        flame.SetActive(false);
    }
    

    private void Relight()
    {
        if (useable)
        {
            flame.SetActive(true);
            torchHits = maxHits;
        }
    }

    private void HitEnemy(GameObject target)
    {
        if (torchHits <= 0)
        {
            return;
        }
        Enemy enemy = target.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.Hurt(torchDamage);
        }
        HandleHitCount();
    }

    private void HitProjectile(GameObject target)
    {
        GhostBlast blast = target.GetComponent<GhostBlast>();
        if (blast != null)
        {
            blast.HitByPlayer(torchDamage);
        }
        torchHits = 0;
    }

    private void HandleHitCount()
    { 
        torchHits--;
        if (torchHits <= 0)
        {
            flame.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Campfire") && useable)
        {
            Relight();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (torchHits <= 0 || !useable)
        {
            return;
        }
        switch (collision.collider.tag)
        {
            case "Enemy":
                HitEnemy(collision.gameObject);
                break;
            case "Projectile":
                HitProjectile(collision.gameObject);
                break;
            default:
                break;
        }
    }

    public void SetMaxHits(int max)
    {
        maxHits = max;
    }

    public void SetTorchDamage(float dmg)
    {
        torchDamage = dmg;
    }

    public bool IsLit()
    {
        return torchHits > 0;
    }
}
