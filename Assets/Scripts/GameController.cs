using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class GameController : ABaseController
{
    [Header("Private")]
    [SerializeField] private int mMoneyToAddOnKill;
    [SerializeField] private int mMoneyToAddOnWaveCompletion;


    [SerializeField] private ResourceManager mResourceManager;
    [SerializeField] private TowerPlacer mTowerPlacerComp;
    private ControllerInputContext mContext;
    private PlayerHUD mPlayerHUD;

    [SerializeField] private AudioSource mGameMusic;
    [SerializeField] private AudioSource mWaveMusic;

    #region Initialisation
    private void Awake()
    {
        if (mTowerPlacerComp == null) mTowerPlacerComp = FindObjectOfType<TowerPlacer>();
        if (mTowerPlacerComp == null) Debug.LogError("Tower Placer reference not set in GameController.");

        DelegateManager.OnWaveComplete += OnWaveComplete;
        DelegateManager.OnEnemyKilled += OnEnemyKilled;
        DelegateManager.OnMakeDecision += OnDecisionMade;

        mGameMusic.Play();
    }

    private void Start()
    {
        mPlayerHUD = OnRequestUI?.Invoke(EUI.PLAYER_HUD).GetWidgetAs<PlayerHUD>();
        if (mPlayerHUD != null)
        {
            mPlayerHUD.Init(this);
            mPlayerHUD.BuyItemRequest += OnItemBought;
            mPlayerHUD.AddToViewport();
        }
    }

    private void OnDestroy()
    {
        DelegateManager.OnWaveComplete -= OnWaveComplete;
        DelegateManager.OnEnemyKilled -= OnEnemyKilled;
        DelegateManager.OnMakeDecision -= OnDecisionMade;
    }

    private void OnDecisionMade(bool hasWon)
    {
        DelegateManager.OnMakeDecision -= OnDecisionMade;

        DecisionBoard decisionUI = OnRequestUI?.Invoke(EUI.DECISION_MENU).GetWidgetAs<DecisionBoard>();
        if(decisionUI != null)
        {
            decisionUI.Init(hasWon);
            decisionUI.AddToViewport();
        }
    }

    protected override void SetupInputComponent()
    {
        mContext = new ControllerInputContext();

        mContext.Default.Pause.performed += PauseGame;
        mContext.Enable();
    }
    #endregion

    #region Inputs
    private void PauseGame(InputAction.CallbackContext obj)
    {
        PauseMenu pauseMenu = OnRequestUI?.Invoke(EUI.PAUSE_MENU).GetWidgetAs<PauseMenu>();
        if (pauseMenu != null)
        {
            pauseMenu.OnResumeGame += ResumeGame;
            pauseMenu.AddToViewport();
        }

        Time.timeScale = 0;
        mContext.Default.Disable();
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
        mContext.Default.Enable();
    }
    #endregion

    private void OnEnemyKilled(bool bAreAllEnemiesDead)
    {
        int ammountToAdd = (bAreAllEnemiesDead) ? mMoneyToAddOnWaveCompletion : mMoneyToAddOnKill;
        ResourceManager.Instance.AddResources(ammountToAdd);
    }
    private void OnWaveComplete(int wave)
    {
        mPlayerHUD.UpdateWaveDetails(wave);
        SoundManager.SwapAudio(mWaveMusic, mGameMusic);

    }

    private void OnItemBought(TowerAsset asset)
    {
        mTowerPlacerComp.TogglePlacingMode(asset.ID);
    }

    public void WaveStartRequest()
    {
        DelegateManager.InvokeWaveStart();

        SoundManager.SwapAudio(mGameMusic, mWaveMusic);
    }


}
