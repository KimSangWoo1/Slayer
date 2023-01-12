using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eUI_TYPE
{
     
}


public interface IWindow 
{
    void Initialize();
    void Destroy();
    void Open();
    void Close();
    void Hide();
    void Back();
}
