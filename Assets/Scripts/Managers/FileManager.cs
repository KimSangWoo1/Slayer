using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft;
using Newtonsoft.Json;

public class FileManager : MonoSingleton<FileManager>
{
    const string EDITOR_PATH = "Assets/AddressableBundle/Data";
    const string SEARH_PATTERN = "*.json";
    
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
        string[] files = Directory.GetFiles(EDITOR_PATH, SEARH_PATTERN, SearchOption.AllDirectories);
        foreach (string file in files)
        {
            string fileName =  file.Substring(file.LastIndexOf('\\')+1);
            Debug.Log($"File Read : {fileName}");


            //TextAsset jsonData = Resources.Load<TextAsset>("Data/GameData");
            
            //JsonDeserialize()
            //GameData gameData = JsonConvert.DeserializeObject<GameData>(jsonData.text);
            //DataManager.Instance.SetFoodList(gameData.Foods);
        }



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
