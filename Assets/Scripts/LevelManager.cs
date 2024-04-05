using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField] private ABaseController ControllerPrefab;

    public GridManager mGridManager;


    private void Awake()
    {
        if(instance == null) instance = this;
    }
    public ABaseController GetControllerClass()
    {
        return ControllerPrefab;
    }
}
