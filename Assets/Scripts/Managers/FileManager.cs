using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft;
using Newtonsoft.Json;

public class FileManager : MonoSingleton<FileManager>
{
    #region File IO
    public static void WriteData(string jsonData)
    {

    }

    public static void LoadData()
    {
        LoadGameData();
    }

    public static void LoadGameData()
    {
#if UNITY_EDITOR


        Debug.Log("Game Data Load");
#elif UNITY_ANDROID

#endif
    }

    #endregion

    private void JsonDeserialize<T>(string className) where T : class
    {
        switch (className)
        {
            case "GameData":
                break;
        }
    }

    private T GetDataClass<T>(string className) where T : class
    {
        T dataClass = null;

        return dataClass;
    }
}
