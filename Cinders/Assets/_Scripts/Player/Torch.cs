using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    [SerializeField] GameObject flame;
    private int hitsLeft;
    public bool useable;
    TorchSO torch;
    List<Upgrade> torchUpgrades;

    private void Start()
    {
        GameManager.instance.newRun += (RunSettings obj) => LoadTorch(obj.torchBase);
        PutOut();
    }

    public void LoadTorch(TorchSO scriptableObj)
    {
        torch = Instantiate(scriptableObj);
        PutOut();
        torchUpgrades = new List<Upgrade>();
    }

    public void AddUpgrade(Upgrade upgrade)
    {
        if (upgrade is InstantUpgrade)
        {
            InstantUpgrade instant = upgrade as InstantUpgrade;
            instant.ApplyUpgrade(torch);
        }
        else if (upgrade is ActiveUpgrade)
        {
            
        }
        torchUpgrades.Add(upgrade);
    }

    private void PutOut()
    {
        hitsLeft = 0;
        flame.SetActive(false);
    }

    private void Relight()
    {
        if (useable)
        {
            flame.SetActive(true);
            hitsLeft = torch.maxHits;
            foreach (ActiveUpgrade upgrade in torch.OnLight)
            {
                if (upgrade.CheckCondition(data: null))
                {
                    upgrade.ActivateEffect();
                }
            }  
        }
    }

    private void HitEnemy(GameObject target)
    {
        if (hitsLeft <= 0)
        {
            return;
        }
        Enemy enemy = target.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.Hurt(torch.damage);
            foreach (ActiveUpgrade upgrade in torch.OnHit)
            {
                if (upgrade.CheckCondition(data: true))
                {
                    upgrade.ActivateEffect();
                }
            }
        }
        HandleHitCount();
    }

    private void HitProjectile(GameObject target)
    {
        GhostBlast blast = target.GetComponent<GhostBlast>();
        if (blast != null)
        {
            blast.HitByPlayer(torch.damage);
            foreach (ActiveUpgrade upgrade in torch.OnHit)
            {
                if (upgrade.CheckCondition(data: true))
                {
                    upgrade.ActivateEffect();
                }
            }
        }
        hitsLeft = 0;
    }

    private void HandleHitCount()
    { 
        hitsLeft--;
        if (hitsLeft <= 0)
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

    private void ReplaceFlame(GameObject flamePrefab)
    {
        Instantiate(torch.flamePrefab, flame.transform);
        Destroy(flame);
        flame = torch.flamePrefab;
        flame.gameObject.transform.SetParent(gameObject.transform);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hitsLeft <= 0 || !useable)
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
                foreach (ActiveUpgrade upgrade in torch.OnHit)
                {
                    if (upgrade.CheckCondition(data: false))
                    {
                        upgrade.ActivateEffect();
                    }
                }
                break;
        }
    }

    public bool IsLit()
    {
        return hitsLeft > 0;
    }
}
