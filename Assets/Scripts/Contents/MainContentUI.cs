using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainContentUI : ContentBaseUI
{
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
