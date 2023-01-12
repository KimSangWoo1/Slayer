using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MileStoneManager : MonoSingleton<MileStoneManager>
{
    public Dictionary<string, MileStone> mileStoneDic = new Dictionary<string, MileStone>();

    public void SetMileStoneContent(string key, eMileStone_Type mileStoneType, int number,  eMileStone_Criteria mileStoneCriteria, int step)
    {
        mileStoneDic[key] = new MileStone(key, mileStoneType, number, mileStoneCriteria, step);
    }

    public eMileStone_Type GetMileStoneContentType(string key)
    {
        if (!mileStoneDic.ContainsKey(key))
        {
            return mileStoneDic[key].MileStoneType;
        }
        return eMileStone_Type.NONE;
    }

    public int GetMileStoneNumber(string key)
    {
        if (!mileStoneDic.ContainsKey(key))
        {
            return mileStoneDic[key].MileStoneNumber;
        }
        return 0;
    }

    public MileStone GetMileStone(string key)
    {
        if (!mileStoneDic.ContainsKey(key))
        {
            return mileStoneDic[key];
        }
        return new MileStone();
    }
}
public enum eMileStone_Criteria
{
    NONE =0,
    LEVEL,
    CONDITION
}

public enum eMileStone_Type
{
        
    NONE =0,

    //Lobby
    SHOP,
    INVENTORY,
    CARD,
    STAGE,
    BATTLE,
    SEASONPASS,
    MAIL,
    CHEST
}
