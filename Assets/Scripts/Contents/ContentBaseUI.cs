using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBaseUI : MonoBehaviour
{
    [SerializeField]
    private bool m_isShow;

    [SerializeField]
    private bool m_isLock;

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

}
