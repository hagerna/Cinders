using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/Enemy", order = 1)]
public class EnemySO : ScriptableObject
{
    public float baseSpeed;
    public float baseHealth;
}
