using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : BaseWidget
{
    public delegate void FOnResumeGameSignature();
    public FOnResumeGameSignature OnResumeGame;

    [SerializeField] private Button btnResume;
    [SerializeField] private Button btnExitToMainMenu;
    [SerializeField] private Button btnQuit;
    private void Awake()
    {
        btnResume.onClick.AddListener(OnResumeButtonClick);
        btnExitToMainMenu.onClick.AddListener(ReturnToMainMenu);
        btnQuit.onClick.AddListener(QuitGame);
    }

    private void OnResumeButtonClick()
    {
        OnResumeGame?.Invoke();
        DestroyWidget();
    }
}
