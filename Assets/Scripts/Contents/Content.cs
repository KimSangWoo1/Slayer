using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Content : GameData
{
    [HideInInspector]
    public int MetaID;
    public eContentType ContentType;
    public eContentOpenType ContentOpenType;
    public MainContent.eMainContentType MainContentType;
    [HideInInspector]
    public string Name;
    [HideInInspector]
    public int OpenLevel;
    [HideInInspector]
    public int OpenQuestID;

    [HideInInspector]
    public string LockText;
    [HideInInspector]
    public string LockNotice;

    public Content() { }

    public Content(int metaID, eContentType contentType, eContentOpenType contentOpenType, MainContent.eMainContentType mainContentType, string name, int openLevel, int openQuestID, string lockText, string lockNotice)
    {
        this.MetaID = metaID;
        this.ContentType = contentType;
        this.ContentOpenType = contentOpenType;
        this.MainContentType = mainContentType;
        this.Name = name;
        this.OpenLevel = openLevel;
        this.OpenQuestID = openQuestID;
        this.LockText = lockText;
        this.LockNotice = lockNotice;
    }
}