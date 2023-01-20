using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JData : IJData, IDisposable
{
    public eGameDataType gameDataType;
    public List<GameData> dataList = new List<GameData>();

    public virtual void Init() {}

    public void Release()
    {
        dataList.Clear();
        dataList = null;
        Dispose();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

}
