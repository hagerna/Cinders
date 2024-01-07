using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    RunSettings currentRun;
    Torch activeTorch;
    FlameHand activeFlameHand;
    Campfire activeCampfire;
    [SerializeField] AnimationCurve torchDamageCurve, fireboltDamageCurve, enemyHealthCurve;
    // Start is called before the first frame update
    void Start()
    {
        StartNewRun();
    }

    private void StartNewRun()
    {
        currentRun = new RunSettings();
        if (!activeTorch)
        {
            activeTorch = FindObjectOfType<Torch>();
        }
        if (!activeFlameHand)
        {
            activeFlameHand = FindObjectOfType<FlameHand>();
        }
        if (!activeCampfire)
        {
            activeCampfire = FindObjectOfType<Campfire>();
        }
        InitializeTorch();
        InitializeFlameHand();
        InitializeCampfire();
    }

    private void InitializeTorch()
    {
        if (activeTorch != null)
        {
            activeTorch.SetMaxHits(currentRun.torchMaxHits);
            activeTorch.SetTorchDamage(torchDamageCurve.Evaluate(currentRun.torchLvl));
        }
    }

    private void InitializeFlameHand()
    {
        if (activeFlameHand)
        {
            activeFlameHand.SetThrowSpeed(currentRun.fireboltThrowSpeed);
            activeFlameHand.SetPullSpeed(currentRun.fireboltPullSpeed);
            activeFlameHand.SetFireboltDamage(fireboltDamageCurve.Evaluate(currentRun.fireboltLvl));
        }
    }

    private void InitializeCampfire()
    {
        if (activeCampfire)
        {
            activeCampfire.SetMaxHealth(currentRun.campfireMaxHealth);
        }
    }

    public float GetEnemyHealth(string EnemyType, bool isElite)
    {
        float value;
        switch (EnemyType)
        {
            default:
                value = enemyHealthCurve.Evaluate(currentRun.night);
                if (isElite)
                    return value * 1.75f;
                break;

        }
        return value;
    }

    public RunSettings GetCurrentRun()
    {
        return currentRun;
    }
}
