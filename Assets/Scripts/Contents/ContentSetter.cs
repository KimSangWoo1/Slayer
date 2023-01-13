using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSetter : MonoBehaviour
{
    [SerializeField]
    private Content m_content;
    [SerializeField]
    private ContentBaseUI m_reference;

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

    private void OnEnable()
    {
        if (m_reference != null)
        {
            //SettingContent();
            m_reference.Set(m_content);
        }
    }

    private void SettingContent()
    {
        if (!string.IsNullOrWhiteSpace(m_content.ContentType.ToKeyString()))
        {
            Content content = ContentsManager.Instance.GetContent(m_content.ContentType.ToKeyString());
            
            CheckContentGuide(content.ContentType);
            CheckContentOpen(content);
            m_content = content;
        }
        else
        {
            Debug.LogWarning($"Not Inspector Setting Content Key : {this.gameObject.name}");
        }
    }

    private void CheckContentOpen(Content content)
    {
        switch (content.ContentOpenType)
        {
            case eContentOpenType.NONE:
                Debug.LogWarning($"Not Setting Content Open Type : {this.gameObject.name}");
                break;
            case eContentOpenType.ALLWAYS:
                m_reference.IsShow = true;
                break;
            case eContentOpenType.LEVEL:
                m_reference.IsShow = true;
                m_reference.IsLock = CheckContentOpenLevel(content.OpenLevel);
                break;
            case eContentOpenType.QUEST:
                m_reference.IsShow = QuestManager.Instance.CheckQuest(content.OpenQuestID);
                break;
            case eContentOpenType.DEVELOPING:
                m_reference.IsShow = true;
                break;
        }
    }

    private void CheckContentGuide(eContentType contentType)
    {
        eGuideStep guideStep = GuideManager.Instance.CheckGuide(contentType);
        switch (guideStep)
        {
            case eGuideStep.NONE:
                m_reference.IsLock = false;
                break;
            case eGuideStep.YET:
                m_reference.IsLock = true;
                break;
            case eGuideStep.START:
                m_reference.IsLock = false;
                GuideManager.Instance.OnGuide(contentType);
                break;
            case eGuideStep.END:
                m_reference.IsLock = false;
                break;
        }
    }

    private bool CheckContentOpenLevel(int openLevel)
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
