using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.Find(typeof(T).Name) as T;

                if(_instance == null)
                {
                    _instance = Activator.CreateInstance<T>();
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
        Debug.Log($"Singleton Manager : { typeof(T).Name}");
    }

    public virtual void Initialize()
    {

    }
}
