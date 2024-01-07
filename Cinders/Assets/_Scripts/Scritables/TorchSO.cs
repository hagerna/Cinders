using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchSO : ScriptableObject
{
    torchStats stats;
    GameObject prefab;

    public void OnPull() { }

    public void OnThrow() { }

    public void OnHit(bool isEnemy) { }
}

public struct torchStats
{
    int hitsLeft;
    int maxHits;
    float damage;
}
