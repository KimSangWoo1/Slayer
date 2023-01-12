using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MileStoneSetter : MonoBehaviour
{
    [SerializeField]
    private MileStone m_mileStone;

    private void Awake()
    {
        CheckMileStone();
    }

    private void OnEnable()
    {
        SettingMileStone();
    }

    private void SettingMileStone()
    {
        if (!string.IsNullOrWhiteSpace(m_mileStone.Key))
        {
            MileStone mileStone = MileStoneManager.Instance.GetMileStone(m_mileStone.Key);
            if (mileStone.MileStoneNumber != 0 && mileStone.MileStoneType != eMileStone_Type.NONE)
            {
                m_mileStone.MileStoneType = mileStone.MileStoneType;
                m_mileStone.MileStoneNumber = mileStone.MileStoneNumber;
                m_mileStone.MileStoneCriteria = mileStone.MileStoneCriteria;
                m_mileStone.DetailStep = mileStone.DetailStep;
            }
            else
            {
                MileStoneManager.Instance.SetMileStoneContent(m_mileStone.Key, m_mileStone.MileStoneType, m_mileStone.MileStoneNumber, m_mileStone.MileStoneCriteria, m_mileStone.DetailStep);
            }
        }
        else
        {
            Debug.LogWarning($"Not Found MileStone Content : {this.gameObject.name}");
        }
    }

    private void CheckMileStone()
    {
        if (!string.IsNullOrWhiteSpace(m_mileStone.Key))
        {
            MileStone mileStone = MileStoneManager.Instance.GetMileStone(m_mileStone.Key);

            switch (mileStone.MileStoneCriteria)
            {
                case eMileStone_Criteria.NONE:
                    SetNoneMileStone(mileStone);
                    break;
                case eMileStone_Criteria.LEVEL:
                    SetLevelMileStone(mileStone);
                    break;
                case eMileStone_Criteria.CONDITION:
                    SetConditionMileStone(mileStone);
                    break;
            }
        }
        else
        {
            Debug.LogWarning($"Not Found MileStone Content : {this.gameObject.name}");
        }
    }

    private void SetLevelMileStone(MileStone mileStone)
    {
        if (mileStone.MileStoneNumber != 0 && mileStone.MileStoneType != eMileStone_Type.NONE)
        {
            m_mileStone.MileStoneType = mileStone.MileStoneType;
            m_mileStone.MileStoneNumber = mileStone.MileStoneNumber;
            m_mileStone.MileStoneCriteria = mileStone.MileStoneCriteria;
            m_mileStone.DetailStep = mileStone.DetailStep;
        }
        else
        {
            MileStoneManager.Instance.SetMileStoneContent(m_mileStone.Key, m_mileStone.MileStoneType, m_mileStone.MileStoneNumber, m_mileStone.MileStoneCriteria, m_mileStone.DetailStep);
        }
    }

    private void SetNoneMileStone(MileStone mileStone)
    {
        if (mileStone.MileStoneNumber != 0 && mileStone.MileStoneType != eMileStone_Type.NONE)
        {
            m_mileStone.MileStoneType = mileStone.MileStoneType;
            m_mileStone.MileStoneNumber = mileStone.MileStoneNumber;
            m_mileStone.MileStoneCriteria = mileStone.MileStoneCriteria;
            m_mileStone.DetailStep = mileStone.DetailStep;
        }
        else
        {
            MileStoneManager.Instance.SetMileStoneContent(m_mileStone.Key, m_mileStone.MileStoneType, m_mileStone.MileStoneNumber, m_mileStone.MileStoneCriteria, m_mileStone.DetailStep);
        }
    }

    private void SetConditionMileStone(MileStone mileStone)
    {

    }
}
