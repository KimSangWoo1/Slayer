using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Content
{
    [HideInInspector]
    public int MetaID;
    public eContentType ContentType;
    public eContentOpenType ContentOpenType;
    [HideInInspector]
    public string Name;
    [HideInInspector]
    public int OpenLevel;
    [HideInInspector]
    public int OpenQuestID;

    [HideInInspector]
    public string BanInfo;
    [HideInInspector]
    public string BanNotice;

    public Content() { }

    public Content(eContentType contentType, eContentOpenType contentOpenType, string name, int openLevel, int openQuestID, string banInfo, string banNotice)
    {
        this.ContentType = contentType;
        this.ContentOpenType = contentOpenType;
        this.Name = name;
        this.OpenLevel = openLevel;
        this.OpenQuestID = openQuestID;
        this.BanInfo = banInfo;
        this.BanNotice = banNotice;
    }
}