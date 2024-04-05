using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DelegateManager
{
    public delegate void FRequestEnemies(int wave);
    public static event FRequestEnemies RequestEnemies;

    public delegate void FOnEnemyKilledSignature(bool bAreAllEnemiesDead);
    public static event FOnEnemyKilledSignature OnEnemyKilled;

    public delegate void FRequestWaveStart();
    public static event FRequestWaveStart RequestWaveStart;

    public delegate void FOnWaveCompleteSignature(int wave);
    public static event FOnWaveCompleteSignature OnWaveComplete;

    public delegate void FOnSceneLoadSignature();
    public static event FOnSceneLoadSignature OnSceneLoad;

    public delegate void FOnSceneUnloadSignature();
    public static event FOnSceneUnloadSignature OnSceneUnload;

    public delegate void FMakeDecisionSignature(bool hasWon);
    public static event FMakeDecisionSignature OnMakeDecision;



    // Decision Invokes
    public static void InvokeDecisionMaker(bool hasWon)
    {
        OnMakeDecision?.Invoke(hasWon);
    }

    // Scene Invokes
    public static void InvokeOnSceneLoad()
    {
        OnSceneLoad?.Invoke();
    }

    public static void InvokeOnSceneUnload()
    {
        OnSceneUnload?.Invoke();
    }


    public static void InvokeEnemies(int wave)
    {
        RequestEnemies?.Invoke(wave);
    }
    public static void InvokeOnEnemyKilled(bool bAreAllEnemiesDead)
    {
        OnEnemyKilled?.Invoke(bAreAllEnemiesDead);
    }
    public static void InvokeWaveStart()
    {
        RequestWaveStart?.Invoke();
    }
    public static void InvokeWaveComplete(int wave)
    {
        OnWaveComplete?.Invoke(wave);
    }
}
