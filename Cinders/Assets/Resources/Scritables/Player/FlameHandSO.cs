using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FlameHand", menuName = "Player/FlameHand", order = 2)]
public class FlameHandSO : ScriptableObject
{
    public bool rightHanded;
    public GameObject fireboltPrefab;

    public float pullSpeed;
    public float throwSpeed;
    public float damage;

    public ActiveUpgrade[] onPull;
    public ActiveUpgrade[] onThrow;
    public ActiveUpgrade[] onHit;
}
