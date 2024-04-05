using UnityEngine;

public abstract class BaseWidget : MonoBehaviour
{
    public delegate void FOnDestroyWidgetSignature(EUI ui);

    public FOnDestroyWidgetSignature OnWidgetDestroy;

    [SerializeField] protected EUI mUiType;
    private void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        
    }
    public void AddToViewport()
    {
        gameObject.SetActive(true);
    }

    public void DestroyWidget()
    {
        OnWidgetDestroy.Invoke(mUiType);
    }
    protected virtual void QuitGame()
    {
        Application.Quit();
    }
    protected virtual void ReturnToMainMenu()
    {
        ASceneManager.instance.Travel(EScene.MAIN_MENU);
    }
    public T GetWidgetAs<T>() where T : BaseWidget
    {
        return this as T;
    }
}
