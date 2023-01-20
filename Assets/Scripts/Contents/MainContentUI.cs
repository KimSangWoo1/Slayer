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

    private void Start()
    {
        
    }

    private void OnDestroy()
    {
        Release();
    }

    #region Override implementations
    public override void Initialize()
    {
        openComplete += OnClick;
    }

    public override void Set<T>(T t)
    {
        
    }

    protected override void OnClick()
    {
        
    }

    protected override void Release()
    {
        openComplete -= OnClick;
    }
    #endregion


}
