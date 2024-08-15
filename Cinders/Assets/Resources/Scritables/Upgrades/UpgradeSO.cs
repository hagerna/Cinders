using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Upgrades/Upgrade", order = 1)]
public class UpgradeSO : ScriptableObject
{
    public string title;
    public string description;
    public float baseValue;
    public UpgradeType type;
    public Sprite iconImage;
    public GameObject vfx;
}

public enum UpgradeType
{
    Torch = 1,
    Firebolt = 2,
    Hearth = 3,
    Rewards = 4,
}
