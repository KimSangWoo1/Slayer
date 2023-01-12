using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MainBaseUI
{
    #region Inspector
    [Header("UI")]
    [SerializeField]
    private Button shopBtn;
    [SerializeField]
    private Button inventoryBtn;
    [SerializeField]
    private Button cardBtn;
    [SerializeField]
    private Button settingBtn;

    [SerializeField]
    private Button stageBtn;
    [SerializeField]
    private Button battleBtn;

    [SerializeField]
    private Button mailBtn;
    [SerializeField]
    private Button rewardBtn;
    [SerializeField]
    private Button seasonPassBtn;

    [SerializeField]
    private Button steminaBtn;
    [SerializeField]
    private Button moneyBtn;
    [SerializeField]
    private Button zamBtn;
    #endregion

    #region Override 
    public override void Initialize()
    {
        CheckMileStone();

        shopBtn.onClick.AddListener(OnShopClick);
        inventoryBtn.onClick.AddListener(OnInventoryClick);
    }

    public override void Destroy()
    {
        throw new System.NotImplementedException();
    }

    public override void CheckMileStone()
    {
        
    }

    public override void Open()
    {
        base.Open();
    }

    public override void Close()
    {
        base.Close();
    }

    public override void Back()
    {
        base.Back();

    }
    #endregion


    #region Mono
    void Update()
    {
        
    }

    #endregion

    #region Button Event
    private void OnShopClick()
    {

    }

    private void OnInventoryClick()
    {

    }
    #endregion

}
