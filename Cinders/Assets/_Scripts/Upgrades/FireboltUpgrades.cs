using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireboltDamageUpgrade : InstantUpgrade
{
    public FireboltDamageUpgrade(Rarity upgradeRarity) : base(Resources.Load<UpgradeSO>("Scritables/Upgrades/FireboltDamage"), upgradeRarity) { }

    public override void Select()
    {
        Object.FindObjectOfType<FlameHand>().AddUpgrade(this);
    }

    public override void ApplyUpgrade(ScriptableObject flamehand)
    {
        FlameHandSO flamehandSO = flamehand as FlameHandSO;
        float multiplier = (float)rarity;
        Debug.Log("Damage multiplier: " + multiplier);
        flamehandSO.damage += baseUpgrade.baseValue * multiplier;
    }
}

public class FireboltPullSpeedUpgrade : InstantUpgrade
{
    public FireboltPullSpeedUpgrade(Rarity upgradeRarity) : base(Resources.Load<UpgradeSO>("Scritables/Upgrades/FireboltPullSpeed"), upgradeRarity) { }

    public override void Select()
    {
        Object.FindObjectOfType<FlameHand>().AddUpgrade(this);
    }

    public override void ApplyUpgrade(ScriptableObject flamehand)
    {
        FlameHandSO flamehandSO = flamehand as FlameHandSO;
        float multiplier = (float)rarity;
        Debug.Log("Pull Speed multiplier: " + multiplier);
        flamehandSO.damage += baseUpgrade.baseValue * multiplier;
    }
}

public class FireboltThrowSpeedUpgrade : InstantUpgrade
{
    public FireboltThrowSpeedUpgrade(Rarity upgradeRarity) : base(Resources.Load<UpgradeSO>("Scritables/Upgrades/FireboltThrowSpeed"), upgradeRarity) { }

    public override void Select()
    {
        Object.FindObjectOfType<FlameHand>().AddUpgrade(this);
    }

    public override void ApplyUpgrade(ScriptableObject flamehand)
    {
        FlameHandSO flamehandSO = flamehand as FlameHandSO;
        float multiplier = (float)rarity;
        Debug.Log("Throw Speed multiplier: " + multiplier);
        flamehandSO.damage += baseUpgrade.baseValue * multiplier;
    }
}
