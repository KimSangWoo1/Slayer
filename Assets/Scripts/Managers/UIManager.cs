using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    public void Open<T>() where T : MonoBehaviour
    {

    }

    public void Close<T>() where T : MonoBehaviour
    {

    }

    public void OpenPopUp(ePopUpType popUpType, string noticeText)
    {

    }
}

public enum ePopUpType
{
    Normal,
    Notice
}

