using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MileStone
{
    [SerializeField]
    private string m_key;

    [SerializeField]
    eMileStone_Criteria m_mileStoneCriteria;
    [SerializeField]
    int m_detailStep;

    [SerializeField]
    eMileStone_Type m_mileStoneType;
    [SerializeField]
    int m_mileStoneNumber;

    // Instance
    public MileStone() {}
    public MileStone(string key, eMileStone_Type mileStoneType, int number,  eMileStone_Criteria mileStoneCriteria, int step)
    {
        this.m_key = key;
        this.m_mileStoneType = mileStoneType;
        this.m_mileStoneNumber = number;
        this.m_mileStoneCriteria = mileStoneCriteria;
        this.m_detailStep = step;
    }

    #region Get Set Properties
    public string Key
    {
        get { return m_key; }
        set { m_key = value; }
    }

    public eMileStone_Type MileStoneType
    {
        get { return m_mileStoneType; }
        set { m_mileStoneType = value; }
    }

    public int MileStoneNumber
    {
        get { return m_mileStoneNumber; }
        set { m_mileStoneNumber = value; }
    }

    public eMileStone_Criteria MileStoneCriteria
    {
        get { return m_mileStoneCriteria; }
        set { m_mileStoneCriteria = value; }
    }

    public int DetailStep
    {
        get { return m_detailStep; }
        set { m_detailStep = value; }
    }
    #endregion
}

