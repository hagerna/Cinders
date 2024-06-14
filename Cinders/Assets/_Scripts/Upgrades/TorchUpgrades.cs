using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchDamageUpgrade : InstantUpgrade
{
    public TorchDamageUpgrade(Rarity upgradeRarity) : base(Resources.Load<UpgradeSO>("Scritables/Upgrades/TorchDamage"), upgradeRarity) { }

    public override void Select()
    {
        Object.FindObjectOfType<Torch>().AddUpgrade(this);
    }

    public override void ApplyUpgrade(ScriptableObject torch)
    {
        TorchSO torchSO = torch as TorchSO;
        float multiplier = (float)rarity;
        Debug.Log("Damage multiplier: " + multiplier);
        torchSO.damage += baseUpgrade.baseValue * multiplier;
    }
}

public class TorchHitsUpgrade : InstantUpgrade
{
    public TorchHitsUpgrade(Rarity upgradeRarity) : base(Resources.Load<UpgradeSO>("Scritables/Upgrades/TorchHits"), upgradeRarity) { }

    public override void Select()
    {
        Object.FindObjectOfType<Torch>().AddUpgrade(this);
    }

    public override void ApplyUpgrade(ScriptableObject torch)
    {
        TorchSO torchSO = torch as TorchSO;
        float multiplier = (float)rarity;
        Debug.Log("Hits multiplier: " + multiplier);
        torchSO.maxHits += Mathf.RoundToInt(baseUpgrade.baseValue * multiplier);
    }
}
