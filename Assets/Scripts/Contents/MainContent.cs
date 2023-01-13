using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainContent : Content
{
    public eContentMainType ContentMainType;
}

public enum eContentMainType
{
    NONE = 0,

    //Lobby
    SHOP,
    INVENTORY,
    CARD,
    STAGE,
    BATTLE,
    SEASONPASS,
    MAIL,
    CHEST
}