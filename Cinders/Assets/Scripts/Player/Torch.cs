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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Campfire"))
        {
            Relight();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            HitEnemy(collision.gameObject);
        }
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

    private void HandleHitCount()
    { 
        TorchHits--;
        if (TorchHits <= 0)
        {
            Flame.SetActive(false);
        }
    }
}
