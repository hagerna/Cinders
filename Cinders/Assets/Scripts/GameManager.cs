using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    RunSettings Current;
    Torch ActiveTorch;
    FlameHand ActiveFlameHand;
    Campfire ActiveCampfire;
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
        if (ActiveFlameHand == null)
        {
            ActiveFlameHand = FindObjectOfType<FlameHand>();
        }
        if (ActiveCampfire == null)
        {
            ActiveCampfire = FindObjectOfType<Campfire>();
        }
        InitializeTorch();
        InitializeFlameHand();
        InitializeCampfire();
    }

    private void InitializeTorch()
    {
        if (ActiveTorch != null)
        {
            ActiveTorch.SetMaxHits(Current.TorchMaxHits);
            ActiveTorch.SetTorchDamage(TorchDamageCurve.Evaluate(Current.TorchLvl));
        }
    }

    private void InitializeFlameHand()
    {
        if (ActiveFlameHand != null)
        {
            ActiveFlameHand.SetThrowSpeed(Current.FireboltThrowSpeed);
            ActiveFlameHand.SetPullSpeed(Current.FireboltPullSpeed);
            ActiveFlameHand.SetFireboltDamage(FireboltDamageCurve.Evaluate(Current.FireboltLvl));
        }
    }

    private void InitializeCampfire()
    {
        if (ActiveCampfire != null)
        {
            ActiveCampfire.SetMaxHealth(Current.CampfireMaxHealth);
        }
    }

    public float GetEnemyHealth(string EnemyType, bool isElite)
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
