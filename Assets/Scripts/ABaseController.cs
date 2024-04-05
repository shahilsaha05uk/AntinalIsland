using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class ABaseController : MonoBehaviour
{
    public delegate BaseWidget FOnRequestUISignature(EUI ui);
    public FOnRequestUISignature OnRequestUI;

    public delegate void FOnSceneChangeRequestSignature(EScene scene);
    public FOnSceneChangeRequestSignature OnSceneChangeRequest;

    protected GameManager mGameManager;

    public virtual void OnSpawn(GameManager manager)
    {
        mGameManager = manager;
        SetupInputComponent();
    }

    protected virtual void SetupInputComponent()
    {

    }

    protected void OnSceneChange(EScene scene)
    {
        OnSceneChangeRequest?.Invoke(scene);
    }
}
