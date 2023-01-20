using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class DataManager : MonoSingleton<DataManager>
{
    #region MainGameData Logic

    private void SetGameData<T>(eGameDataType gameDataType, T data) where T : JData
    {
        switch (gameDataType)
        {
            case eGameDataType.MainContent:
                SetContentsData(data);
                break;
            case eGameDataType.Guide:
                SetGuidData();
                break;
            case eGameDataType.User:
                SetUserData();
                break;
        }
    }

    #region Contents
    private void SetContentsData(JData mainContentsJData)
    {
        mainContentsJData = mainContentsJData as MainContentsJData;
    }

    #endregion

    #region Guide
    private void SetGuidData()
    {

    }

    #endregion

    #region User
    private void SetUserData()
    {

    }

    #endregion
    #endregion
}
