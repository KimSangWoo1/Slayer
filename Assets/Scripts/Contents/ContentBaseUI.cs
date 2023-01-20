using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class ContentBaseUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    protected Image _backImage;
    [SerializeField]
    protected Image _mainImage;
    [SerializeField]
    protected TextMeshProUGUI _nameTxt;
    [SerializeField]
    protected Button _btn;

    [Header("Option")]
    [SerializeField]
    protected bool _isShow;
    [SerializeField]
    protected bool _isLock;

    // Event
    protected delegate void OpenComplete();

    #region Get Set Properties
    public bool IsShow
    {
        get { return _isShow; }
        set { _isShow = value; }
    }

    public bool IsLock
    {
        get { return _isLock; }
        set { _isLock = value; }
    }
    #endregion

    public abstract void Initialize();

    public abstract void Set<T>(T t) where T : Content;

    protected abstract void Release();

    protected abstract void OnClick();
}
