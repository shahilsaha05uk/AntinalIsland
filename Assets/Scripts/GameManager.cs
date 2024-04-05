using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour, ISceneUnloadInterface, ISceneLoadInterface
{
    public static GameManager instance;

    public UIManager mUIManager;
    public ASceneManager mSceneManager;
    public WaveManager mWaveManager;
    public EnemyManager mEnemyManager;

    private ABaseController mController;

    private IDictionary<Food, int> mFoodPileDictionary;

    private void Awake()
    {
        if(instance == null) instance = this;

        mFoodPileDictionary = new Dictionary<Food, int>();

        DelegateManager.OnSceneUnload += OnSceneUnload;
        DelegateManager.OnSceneLoad += OnSceneLoad;
        DelegateManager.OnEnemyKilled += OnEnemyKilled;
    }

    private void Start()
    {
        InitialiseController();
    }

    private void OnDestroy()
    {
        DelegateManager.OnSceneUnload -= OnSceneUnload;
        DelegateManager.OnSceneLoad -= OnSceneLoad;
        DelegateManager.OnEnemyKilled -= OnEnemyKilled;
    }

    private void InitialiseController()
    {
        ABaseController Controller = LevelManager.instance.GetControllerClass();
        if (Controller != null)
        {
            mController = Instantiate(Controller, Vector3.zero, Quaternion.identity);
            mController.OnRequestUI += OnRequestUI;
            mController.OnSceneChangeRequest += ChangeScene;
            mController.OnSpawn(this);
        }
    }

    public void RegisterFoodPile(Food pile, int quantity)
    {
        pile.OnFoodConsumed += OnFoodConsumed;
        mFoodPileDictionary.Add(pile, quantity);
    }

    private void OnFoodConsumed(Food pile, int remainingQuantity)
    {
        mFoodPileDictionary[pile] = remainingQuantity;

        if (remainingQuantity <= 0)
        {
            pile.gameObject.SetActive(false);
        }
        if (mFoodPileDictionary.Count <= 0)
        {
            DelegateManager.InvokeDecisionMaker(false);
        }
    }


    private void OnEnemyKilled(bool bAreAllEnemiesDead)
    {
        if (bAreAllEnemiesDead)
        {
            DelegateManager.InvokeWaveComplete(mWaveManager.mCurrentWave);
            if (mWaveManager.mCurrentWave >= mWaveManager.mMaxWave)
            {
                DelegateManager.InvokeDecisionMaker(true);
            }

        }
    }
    private BaseWidget OnRequestUI(EUI ui)
    {
        return mUIManager.InitialiseWidget(ui);
    }

    private void ChangeScene(EScene scene)
    {
        mSceneManager.Travel(scene);
    }

    // Scene Inits
    public void OnSceneLoad()
    {
        InitialiseController();

/*        if (mFoodPileDictionary != null)
        {
            foreach (var o in mFoodPileDictionary)
            {
                o.Key.gameObject.SetActive(true);
            }
        }*/
    }
    public void OnSceneUnload()
    {
        mController.OnRequestUI = null;
        mController.OnSceneChangeRequest = null;
        Destroy(mController);
    }
}
