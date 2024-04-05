using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ASceneManager : MonoBehaviour
{
    public static ASceneManager instance;

    private void Awake()
    {
        if(instance == null) instance = this;
    }

    public void Travel(EScene sceneToLoad)
    {
        string pathToScene = SceneUtility.GetScenePathByBuildIndex((int)sceneToLoad);
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(pathToScene);
        StartCoroutine(LoadScene(sceneName));
    }

    private IEnumerator LoadScene(string sceneName)
    {
        DelegateManager.InvokeOnSceneUnload();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        DelegateManager.InvokeOnSceneLoad();
    }
}
