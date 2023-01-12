using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum EcustomPart
{
    None        = 0,
    Head        = 1 << 0,
    Hair        = 1 << 1,
    Hat         = 1 << 2,
    Body        = 1 << 3,
    LeftArm     = 1 << 4,
    RightArm    = 1 << 5
}

public interface IEquip<T>
{
    void Set(T equipType);
    T Get();
}

public class Parts : IEquip<EcustomPart>
{
    public EcustomPart customPart;

    public void Set(EcustomPart equipType)
    {
        customPart = equipType;
    }

    public EcustomPart Get()
    {
        return customPart;
    }

}

