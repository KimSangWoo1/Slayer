using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ContentSetter))]
public abstract class ContentBaseUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    protected Text m_nameTxt;
    [SerializeField]
    protected Button m_btn;

    [SerializeField]
    protected Text m_banTxt;
    [SerializeField]
    protected Image m_lockImg;

    [Header("Option")]
    [SerializeField]
    protected bool m_isShow;
    [SerializeField]
    protected bool m_isLock;

    // Event
    protected delegate void OpenComplete();

    #region Get Set Properties
    public bool IsShow
    {
        get { return m_isShow; }
        set { m_isShow = value; }
    }

    public bool IsLock
    {
        get { return m_isLock; }
        set { m_isLock = value; }
    }
    #endregion

    public abstract void Initialize();

    public abstract void Set<T>(T t) where T : Content;

    protected abstract void Release();

    protected abstract void OnClick();
}
