using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunSettings
{
    public int night;
    public float fireboltLvl, torchLvl;
    public int torchMaxHits;
    public float fireboltThrowSpeed, fireboltPullSpeed;
    public int campfireMaxHealth;

    public RunSettings()
    {
        night = 1;
        fireboltLvl = 1;
        torchLvl = 1;
        torchMaxHits = 3;
        fireboltThrowSpeed = 4;
        fireboltPullSpeed = 2;
        campfireMaxHealth = 5;
    }
}
