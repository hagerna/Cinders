using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    RunSettings Current;
    Torch ActiveTorch;
    FlameHand ActiveFlameHand;
    [SerializeField] AnimationCurve TorchDamageCurve, FireboltDamageCurve, EnemyHealthCurve;
    // Start is called before the first frame update
    void Start()
    {
        StartNewRun();
    }

    private void StartNewRun()
    {
        Current = new RunSettings();
        if (ActiveTorch == null)
        {
            ActiveTorch = FindObjectOfType<Torch>();
        }
        if (ActiveTorch != null)
        {
            ActiveTorch.SetMaxHits(Current.TorchMaxHits);
            ActiveTorch.SetTorchDamage(TorchDamageCurve.Evaluate(Current.TorchLvl));
        }
        if (ActiveFlameHand == null)
        {
            ActiveFlameHand = FindObjectOfType<FlameHand>();
        }
        if (ActiveFlameHand != null)
        {
            ActiveTorch.SetMaxHits(Current.TorchMaxHits);
            ActiveTorch.SetTorchDamage(FireboltDamageCurve.Evaluate(Current.FireboltLvl));
        }
    }

    public float GetEnemyHealth(bool isElite)
    {
        float value = EnemyHealthCurve.Evaluate(Current.Night);
        if (isElite)
            return value * 2;
        else
            return value;
    }

    public RunSettings GetCurrentRun()
    {
        return Current;
    }
}
