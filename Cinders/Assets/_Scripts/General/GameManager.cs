using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    RunSettings currentRun;
    [SerializeField] TorchSO torchDefault;
    [SerializeField] FlameHandSO flameHandDefault;
    [SerializeField] CampfireSO campfireDefault;
    [SerializeField] AnimationCurve enemyHealthCurve;

    public Action<RunSettings> newRun;
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    private void Update()
    {
        OVRInput.Update();
        if (currentRun == null && (OVRInput.Get(OVRInput.Button.One) || Input.GetKeyDown(KeyCode.A)))
        {
            StartNewRun();
        }
    }


    private void StartNewRun()
    {
        currentRun = new RunSettings(torchDefault, flameHandDefault, campfireDefault);
        newRun.Invoke(currentRun);
        Debug.Log("New Run Started");
    }

    public RunSettings GetCurrentRun()
    {
        return currentRun;
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
}
