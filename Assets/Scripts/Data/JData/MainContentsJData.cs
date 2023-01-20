using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainContentsJData : JData
{
    public MainContent Shop;
    public MainContent Inventory;
    public override void Init()
    {
        gameDataType = eGameDataType.MainContent;

        dataList.Add(Shop);
        dataList.Add(Inventory);
    }
}

