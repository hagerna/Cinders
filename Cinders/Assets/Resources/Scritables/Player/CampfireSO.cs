using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Campfire", menuName = "Player/Campfire", order = 3)]
public class CampfireSO : ScriptableObject
{
    public GameObject flamePrefab;

    public int maxLife;

    public ActiveUpgrade[] onTorchLight;
    public ActiveUpgrade[] onHurt;

}
