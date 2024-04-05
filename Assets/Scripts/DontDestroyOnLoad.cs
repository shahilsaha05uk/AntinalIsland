using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    public static GameObject instanceRef;
    private void Awake()
    {
        if (instanceRef == null)
        {
            instanceRef = this.gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else if (instanceRef != this)
            Destroy(gameObject);
    }
}
