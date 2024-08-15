using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public class UpgradeSelector : MonoBehaviour
{
    [SerializeField] UpgradeObject[] options;

    private List<Type>[] upgradeTiers;
    private List<Type>[] currentlyDisplayed;
    private Rarity[] rarities = new Rarity[] { Rarity.Common, Rarity.Uncommon, Rarity.Rare, Rarity.Epic, Rarity.Legendary };

    // Start is called before the first frame update
    void Awake()
    {
        InitializeUpgrades();
    }

    private void InitializeUpgrades()
    {
        // Initialize List Arrays
        upgradeTiers = new List<Type>[3];
        currentlyDisplayed = new List<Type>[] { new List<Type>(), new List<Type>(), new List<Type>() };

        // Set Base Tier 1 Upgrades
        upgradeTiers[0] = new List<Type>() { typeof(TorchDamageUpgrade), typeof(TorchHitsUpgrade), typeof(FireboltDamageUpgrade),
                                                typeof(FireboltPullSpeedUpgrade), typeof(FireboltThrowSpeedUpgrade) };

        // Set Base Tier 2 Upgrades
        upgradeTiers[1] = new List<Type>() { };

        // Set Base Tier 3 Upgrades
        upgradeTiers[2] = new List<Type>() { };
    }

    public void CreateRandomUpgradeOptions()
    {
        AssignRandom(options[0]);
        AssignRandom(options[1]);
        AssignRandom(options[2]);
    }

    public void CreateCustomUpgrades(Type[] types, Rarity[] rarities)
    {

        for (int i = 0; i < 3; i++)
        {
            // Check to make sure upgradeType is in fact a valid UpgradeType
            if (upgradeTiers[0].Contains(types[i]) || upgradeTiers[1].Contains(types[i]) || upgradeTiers[2].Contains(types[i]))
            {
                AssignUpgrade(target: options[i], upgradeType: types[i], rarity: rarities[i]);
            }
        }
    }

    public void ClearUpgradeOptions()
    {
        if (!IsDisplayed())
        {
            return;
        }
        for (int tier = 0; tier < 3; tier++)
        {
            if (currentlyDisplayed[tier].Count > 0)
            {
                foreach (Type upgradeType in currentlyDisplayed[tier])
                {
                    upgradeTiers[tier].Add(upgradeType);
                }
                currentlyDisplayed[tier].Clear();
            }
        }
        foreach(UpgradeObject option in options)
        {
            // The selected option will hide itself after a delay
            if (!option.selecting)
            {
                option.gameObject.SetActive(false);
            }
        }
    }

    public void AssignRandom(UpgradeObject target)
    {
        int randomTier = 0;
        if (upgradeTiers[randomTier].Count == 0)
        {
            Debug.Log("Not enough Upgrades left.");
            return;
        }
        int randomIndex = UnityEngine.Random.Range(0, upgradeTiers[randomTier].Count - 1);
        Type randomUpgradeType = upgradeTiers[randomTier][randomIndex];
        upgradeTiers[randomTier].RemoveAt(randomIndex);
        currentlyDisplayed[randomTier].Add(randomUpgradeType);

        int randomRarityIndex = UnityEngine.Random.Range(0, 4);
        Rarity randomRarity = rarities[randomRarityIndex];

        AssignUpgrade(target, randomUpgradeType, randomRarity);
    }

    public void AssignUpgrade(UpgradeObject target, Type upgradeType, Rarity rarity)
    {
        ConstructorInfo ctor = upgradeType.GetConstructor(new[] { typeof(Rarity) });
        if (ctor != null)
        {
            var upgrade1 = ctor.Invoke(new object[] { rarity });
            if (upgrade1 != null)
            {
                target.SetUpgrade(upgrade1 as Upgrade);
                target.gameObject.SetActive(true);
            }
        }
        else
        {
            Debug.Log("Failed to assign upgrade to " + target);
        }
    }

    public bool IsDisplayed()
    {
        return options[0].isActiveAndEnabled || options[1].isActiveAndEnabled || options[2].isActiveAndEnabled;
    }
}
