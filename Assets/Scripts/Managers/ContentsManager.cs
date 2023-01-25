using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentsManager : MonoSingleton<ContentsManager>
{
    public Dictionary<MainContent.eMainContentType, MainContent> mainContentsDic = new Dictionary<MainContent.eMainContentType, MainContent>();

    #region 
    public void SetMainContent(JData jData)
    {

    }

    public T GetContent<T>(eContentType contentType, MainContent.eMainContentType mainContentType) where T : Content, new()
    {
        T content = new T();
        switch (contentType)
        {
            case eContentType.Main:
                content = GetMainContent(mainContentType) as T;
                break;
            case eContentType.Sub:
                content = GetSubContent(mainContentType) as T;
                break;
            case eContentType.Special:
                content = GetSpecialContent() as T;
                break;
        }
        return content ;
    }

    private MainContent GetMainContent(MainContent.eMainContentType mainContentType)
    {
        if (!mainContentsDic.ContainsKey(mainContentType))
        {
            return mainContentsDic[mainContentType];
        }
        return new MainContent();
    }

    private SubContent GetSubContent(MainContent.eMainContentType mainContentType)
    {
        return new SubContent();
    }

    private SpecialContent GetSpecialContent()
    {
        return new SpecialContent();
    }
    #endregion

    public override void Initialize()
    {
        base.Initialize();

        MainContentsJData datas = DataManager.Instance.GetData<MainContentsJData>(eGameDataType.MainContent);
        datas.Init();
        if (datas != null && datas.dataList.Count > 0)
        {
            for (int i = 0; i < datas.dataList.Count; i++)
            {
                MainContent mainContent = datas.dataList[i] as MainContent;
                mainContentsDic[mainContent.MainContentType] = mainContent;
            }
        }
        datas.Release();
    }
}

public enum eContentOpenType
{
    None = 0,
    Allways,
    Level,
    Quest,
    Developing
}

public enum eContentType
{
    None = 0,

    Main,
    Sub,
    Special
}
