using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainContentSetter : ContentSetter
{
    [SerializeField]
    private MainContent m_content;


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
        if (m_content.ContentType != eContentType.None)
        {
            MainContent content = ContentsManager.Instance.GetContent<MainContent>(m_content.ContentType);

            CheckContentGuide(content.ContentType);
            CheckContentOpen(content);
            m_content = content;
        }
        else
        {
            Debug.LogWarning($"Not Inspector Setting Content Key : {this.gameObject.name}");
        }
    }
}
