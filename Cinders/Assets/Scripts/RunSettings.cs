using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunSettings
{
    public int Night;
    public float FireboltLvl, TorchLvl;
    public int TorchMaxHits;
    public float FireboltThrowSpeed, FireboltPullSpeed;

    public RunSettings()
    {
        Night = 1;
        FireboltLvl = 1;
        TorchLvl = 1;
        TorchMaxHits = 3;
        FireboltThrowSpeed = 4;
        FireboltPullSpeed = 2;
    }
}
