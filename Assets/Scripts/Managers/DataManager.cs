using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public partial class DataManager : MonoSingleton<DataManager>
{
    private Dictionary<eGameDataType, JData> gameDataDic = new Dictionary<eGameDataType, JData>();

    public int GetPlayerLevel()
    {
        return 0;
    }

    #region
    public async UniTaskVoid LoadGameData<T>(string key) where T : JData
    {
        T jData = await JSonManager.LoadJsonData<T>(key);
        gameDataDic.Add(jData.gameDataType, jData);
    }
    #endregion

    public T GetData<T>(eGameDataType gameDataType) where T : JData
    {
        return gameDataDic[gameDataType] as T;
    }

}

public enum eGameDataType
{
    MainContent,
    Guide,
    User
}