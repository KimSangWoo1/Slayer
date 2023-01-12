using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentsManager : MonoSingleton<ContentsManager>
{
    public Dictionary<string, Content> ContentsDic = new Dictionary<string, Content>();
    public List<Content> ContentOpenList = new List<Content>();

    public void SetContent(Content content)
    {
        ContentsDic[content.Key] = content;
    }

    public Content GetContent(string key)
    {
        if (!ContentsDic.ContainsKey(key))
        {
            return ContentsDic[key];
        }
        return new Content();
    }
}

public enum eContentGuide
{
    NONE = 0,
    YET,
    START,
    END
}

public enum eContentOpenType
{
    NONE = 0,
    ALLWAYS,
    LEVEL,
    QUEST,
    DEVELOPING
}

public enum eContentType
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
