using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Torch", menuName = "Player/Torch", order = 1)]
public class TorchSO : ScriptableObject
{
    public GameObject flamePrefab;
    public int maxHits;
    public float damage;
    public float knockback;
    public float critChance;
    public float critDamageModifier;
    public ActiveUpgrade[] OnHit;
    public ActiveUpgrade[] OnLight;
}
