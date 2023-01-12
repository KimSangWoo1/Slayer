using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MainBaseUI : MonoBehaviour, IWindow
{
    public abstract void Initialize();
    public abstract void Destroy();
    public abstract void CheckMileStone();

    public virtual void Open() {}
    public virtual void Close() {}
    public virtual void Hide() { }

    public virtual void Back() { }
}
