using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Content
{
    public string Key;
    public eContentType ContentType;
    public eContentOpenType ContentOpenType;
    public eContentGuide ContentGuide;
    [HideInInspector]
    public int OpenLevel;
    [HideInInspector]
    public int OpenQuestID;
    [HideInInspector]
    public int MaxProgress;
    [HideInInspector]
    public int CurrentProgress;
    [HideInInspector]
    public bool IsGuide;
    [HideInInspector]
    public int MaxGuideProgress;
    [HideInInspector]
    public int CurrentGuideProgress;

    public Content() { }

    public Content(string key, eContentType contentType, eContentOpenType contentOpenType, eContentGuide contentGuide, int openLevel, int openQuestID, int maxProgress, int currentProgress, bool isGuide, int maxGuideProgress, int currentGuideProgress)
    {
        this.Key = key;
        this.ContentType = contentType;
        this.ContentOpenType = contentOpenType;
        this.ContentGuide = contentGuide;
        this.OpenLevel = openLevel;
        this.OpenQuestID = openQuestID;
        this.MaxProgress = maxProgress;
        this.CurrentProgress = currentProgress;
        this.IsGuide = isGuide;
        this.MaxGuideProgress = maxGuideProgress;
        this.CurrentGuideProgress = currentGuideProgress;
    }
}