using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft;
using Newtonsoft.Json;
using Cysharp.Threading.Tasks;

public static class JSonManager 
{
    public static async UniTask<T> LoadJsonData<T>(string key, Action onEvent = null) where T : JData
    {
        Debug.Log("AS Load START");
        TextAsset result = await AddressableManager.Instance.LoadData(key);
        T t;
        if (result == null)
        {
            t = null;
            Debug.Log("≥Œ¿Ã¥Ÿ");
        }
        else
        {
            Debug.Log("AS Load END");
            t = JsonConvert.DeserializeObject<T>(result.text) as T;
        }
        return t;
    }

    public static void WriteJsonData()
    {

    }
}
