using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MainContent : Content
{
    
    public enum eMainContentType
    {
        None = 0,

        //Lobby
        Shop,
        Inventory,
        Card,
        Stage,
        Battle,
        SeasonPass,
        Clan,
        Chest
    }
}
