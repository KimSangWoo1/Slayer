using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MainContentSetter))]

public class MainContentUI : ContentBaseUI
{
    [Header ("Badge")]
    [SerializeField]
    private NotificationBadge _notificationBadge;
    [Header("Lock")]
    [SerializeField]
    private LockUI _lockUI;

    //Event
    OpenComplete openComplete;

    private void OnDestroy()
    {
        Release();
    }

    #region Override implementations
    public override void Initialize()
    {
        openComplete += OnClick;
        _btn.onClick.AddListener(openComplete.Invoke);
    }

    public override void Set<Content>(Content content) 
    {
        if (_isShow)
        {
            MainContent mainContent = content as MainContent;
            _nameTxt.text = mainContent.Name;
            _btn.interactable = !IsLock;
            _lockUI.Set(_isLock, mainContent.LockText, mainContent.LockNotice);
            _notificationBadge.Initialize();
        }
        else
        {
            this.gameObject.SetActive(_isShow);
        }
    }

    protected override void OnClick()
    {
        
    }

    protected override void Release()
    {
        openComplete -= OnClick;
        _btn.onClick.RemoveAllListeners();
    }
    #endregion


}
