using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MainBaseUI
{
    #region Inspector
    [Header("UI")]
    [SerializeField]
    private MainContentUI[] mainContentUIs;
    #endregion

    #region Mono
    private void Awake()
    {
        Initialize();
    }
    private void Start()
    {

    }
    void Update()
    {

    }

    #endregion

    #region Override 
    public override void Initialize()
    {
        CheckMileStone();
        SetDataToContets();
    }

    private void SetDataToContets()
    {
        //AddressableManager.Instance.CheckUpdate();
        List<string> list = new List<string>();
        list.Add("MainContent");
        //action += OnAction;
        //action = () => DataManager.Instance.LoadGameData<MainContentsJData>("MainContent");

        Debug.Log("AAA");
        DataManager.Instance.LoadGameData<MainContentsJData>("MainContent").Forget();
        Debug.Log("BBB");
        //AddressableManager.Instance.Download(list, action);
        //FileManager.LoadData();
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

    #region Button Event
    private void OnShopClick()
    {

    }

    private void OnInventoryClick()
    {

    }
    #endregion

}
