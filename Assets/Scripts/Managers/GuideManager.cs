using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideManager : MonoSingleton<GuideManager>
{
    /*
     *  //public eContentGuide ContentGuide;
[HideInInspector]
public int MaxProgress;
[HideInInspector]
public int CurrentProgress;
[HideInInspector]
public bool IsGuide;
[HideInInspector]
public int MaxGuideProgress;
[HideInInspector]
public int CurrentGuideProgress;
*/

    public void OnGuide(eContentType contentType)
    {

    }

    public eGuideStep CheckGuide(eContentType contentType)
    {
        throw new NotImplementedException();
    }
}

public enum eGuideStep
{
    None = 0,
    Yet,
    Start,
    End
}