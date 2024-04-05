using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

public class EnemyManager : MonoBehaviour, ISceneLoadInterface
{
    private GameManager mGameManager;

    [SerializeField] private AntAI AntPrefab;
    [SerializeField] private float mSpawnRate = 0.3f;
    [SerializeField] private SplineContainer[] mSplines;

    private int mTotalEnemyCount;
    private int mCurrentEnemyCount;
    [SerializeField] private int mEnemyIncreaseCount;

    private void Awake()
    {
        mGameManager = GetComponentInParent<GameManager>();
        DelegateManager.RequestEnemies += SpawnEnemies;
        DelegateManager.OnSceneLoad += OnSceneLoad;
    }


    private void OnDestroy()
    {
        DelegateManager.RequestEnemies -= SpawnEnemies;
    }
    public void OnSceneLoad()
    {
        mSplines = FindObjectsByType<SplineContainer>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
    }

    public void SpawnEnemies(int numberOfEnemiesToSpawn)
    {
        if (mSplines.Length <= 0) return;
        mTotalEnemyCount = numberOfEnemiesToSpawn + mEnemyIncreaseCount;
        mCurrentEnemyCount = mTotalEnemyCount;
        StartCoroutine(Spawner(mTotalEnemyCount));
    }

    private IEnumerator Spawner(int numberOfEnemiesToSpawn)
    {
        WaitForSeconds timeInterval = new WaitForSeconds(mSpawnRate);

        while (numberOfEnemiesToSpawn > 0)
        {
            var spline = mSplines[UnityEngine.Random.Range(0, mSplines.Length)];
            
            AntAI ant = Instantiate(AntPrefab, Vector3.zero, Quaternion.identity, null);
            ant.OnDead += OnEnemyDead;
            ant.OnSpawn(spline);

            numberOfEnemiesToSpawn--;
            yield return timeInterval;
        }
        yield return null;
    }

    private void OnEnemyDead()
    {
        mCurrentEnemyCount--;
        bool areAllDead = (mCurrentEnemyCount <= 0);
        DelegateManager.InvokeOnEnemyKilled(areAllDead);
    }

}
