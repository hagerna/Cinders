using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Upgrade
{
    public UpgradeSO baseUpgrade { get; protected set; }
    public Rarity rarity { get; protected set; }

    public Upgrade(UpgradeSO scriptable, Rarity upgradeRarity)
    {
        baseUpgrade = scriptable;
        rarity = upgradeRarity;
    }

    public string GetModifiedDescription()
    {
        float multiplier = (float)rarity;
        return String.Format(baseUpgrade.description, baseUpgrade.baseValue * multiplier);
    }

    public abstract void Select();
}

public abstract class ActiveUpgrade : Upgrade
{
    public ActiveUpgrade(UpgradeSO scriptable, Rarity upgradeRarity) : base(scriptable, upgradeRarity) { }

    public abstract bool CheckCondition(object data);

    public abstract void ActivateEffect();
}

public abstract class InstantUpgrade : Upgrade
{
    public InstantUpgrade(UpgradeSO scriptable, Rarity upgradeRarity) : base(scriptable, upgradeRarity) { }

    public abstract void ApplyUpgrade(ScriptableObject scriptable);
}

public enum Rarity
{
    Common = 1,
    Uncommon = 2,
    Rare = 3,
    Epic = 4,
    Legendary = 5
}
