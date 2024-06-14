using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour
{
    protected int fireHealth, maxFireHealth;
    [SerializeField] float duration;
    [SerializeField] AnimationCurve flickerCurve;
    public bool immune = false;
    CampfireSO campfire;
    List<Upgrade> campfireUpgrades;

    private void Start()
    {
        GameManager.instance.newRun += (RunSettings obj) => LoadCampfire(obj.campfireBase);
    }

    private void LoadCampfire(CampfireSO scriptableObj)
    {
        campfire = scriptableObj;
    }

    public void AddUpgrade(Upgrade upgrade)
    {
        if (upgrade is InstantUpgrade)
        {
            InstantUpgrade instant = upgrade as InstantUpgrade;
            instant.ApplyUpgrade(campfire);
        }
        campfireUpgrades.Add(upgrade);
    }

    public void SetMaxHealth(int max)
    {
        maxFireHealth = max;
        fireHealth = max;

    }

    public void FireReached()
    {
        if (immune)
        {
            return;
        }
        fireHealth--;
        if (fireHealth > 0)
        {
            StartCoroutine(Flicker());
            foreach (ActiveUpgrade upgrade in campfire.onHurt)
            {
                if (upgrade.CheckCondition(data: null))
                {
                    upgrade.ActivateEffect();
                }
            }
            // Trigger reached effects
        }
        else
        {
            // Game Over
            Destroy(gameObject);
        }
    }


    IEnumerator Flicker()
    {
        float time = 0;
        while (time <= duration)
        {
            // Trigger flickering effect
            time += Time.deltaTime;
        }
        yield return new WaitForEndOfFrame();
    }
}
