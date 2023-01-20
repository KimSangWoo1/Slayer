using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSetter : MonoBehaviour
{
    [SerializeField]
    protected ContentBaseUI m_reference;

    private void Awake()
    {
        if(m_reference == null)
        {
            m_reference = GetComponent<ContentBaseUI>();
            if(m_reference == null)
            {
                Debug.LogWarning($"Not Add ContentBaseUI : {this.gameObject.name}");
                return;
            }
        }
        m_reference.Initialize();
    }

    protected void CheckContentOpen(Content content)
    {
        switch (content.ContentOpenType)
        {
            case eContentOpenType.None:
                Debug.LogWarning($"Not Setting Content Open Type : {this.gameObject.name}");
                break;
            case eContentOpenType.Allways:
                m_reference.IsShow = true;
                break;
            case eContentOpenType.Level:
                m_reference.IsShow = true;
                m_reference.IsLock = CheckContentOpenLevel(content.OpenLevel);
                break;
            case eContentOpenType.Quest:
                m_reference.IsShow = QuestManager.Instance.CheckQuest(content.OpenQuestID);
                break;
            case eContentOpenType.Developing:
                m_reference.IsShow = true;
                break;
        }
    }

    protected void CheckContentGuide(eContentType contentType)
    {
        eGuideStep guideStep = GuideManager.Instance.CheckGuide(contentType);
        switch (guideStep)
        {
            case eGuideStep.None:
                m_reference.IsLock = false;
                break;
            case eGuideStep.Yet:
                m_reference.IsLock = true;
                break;
            case eGuideStep.Start:
                m_reference.IsLock = false;
                GuideManager.Instance.OnGuide(contentType);
                break;
            case eGuideStep.End:
                m_reference.IsLock = false;
                break;
        }
    }

    protected bool CheckContentOpenLevel(int openLevel)
    {
        int pleryLevel = DataManager.Instance.GetPlayerLevel();
        if(pleryLevel >= openLevel)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
