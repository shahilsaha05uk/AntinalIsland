using UnityEngine;
using UnityEngine.UI;

public class MainMenu : BaseWidget
{
    [SerializeField] private Button mPlayButton;
    [SerializeField] private Button mQuitButton;
    
    public delegate void FOnSceneChangeRequest(EScene scene);
    public FOnSceneChangeRequest OnSceneChangeRequest;


    public override void Init()
    {
        base.Init();
        mUiType = EUI.MAIN_MENU;
        
        mPlayButton.onClick.AddListener(OnPlayButtonClick);
        mQuitButton.onClick.AddListener(QuitGame);
    }
    private void OnPlayButtonClick()
    {
        OnSceneChangeRequest?.Invoke(EScene.TestGame);
    }
}
