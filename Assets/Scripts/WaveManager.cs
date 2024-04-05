using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public delegate void FRequestEnemies(int wave);
    public static event FRequestEnemies RequestEnemies;


    public int mCurrentWave{ private set; get; }
    public int mMaxWave = 10;

    private GameManager mGameManager;

    private void Awake()
    {
        mGameManager = GetComponentInParent<GameManager>();
        DelegateManager.RequestWaveStart += InitiateWave;
        mCurrentWave = 1;
    }
    public void InitiateWave()
    {
        DelegateManager.InvokeEnemies(mCurrentWave);

        mCurrentWave++;
    }
}
