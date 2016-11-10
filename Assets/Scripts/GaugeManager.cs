using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeManager : MonoBehaviour {

    /// <summary>
    /// ゲージ制御配列
    /// </summary>
    [SerializeField]
    private GaugeController[] m_GaugeControllers;

    private int m_TurnIndex;

    private void Awake()
    {
        foreach (var gauge in m_GaugeControllers)
        {
            gauge.speed = 100;
        }

        StartCoroutine(GoUpdateGauge());
    }

    /// <summary>
    /// ゲージ更新
    /// </summary>
    private void UpdateGauge()
    {
        foreach (var gauge in m_GaugeControllers)
        {
            gauge.AddGaugeFromRate(ConstTable.gaugeUpdateRate);
        }
    }

    private IEnumerator GoUpdateGauge()
    {
        m_TurnIndex = -1;

        int count = 0;

        while(true)
        {
            if (m_TurnIndex >= 0)
            {
                yield return null;
                continue;
            }

            count++;
            Debug.LogFormat("更新回数：{0}", count);

            // ゲージ更新
            UpdateGauge();

            m_TurnIndex = CheckTurn();

            yield return null;
        }
    }

    private int CheckTurn()
    {
        int index = -1;

        for( int i = 0; i < m_GaugeControllers.Length; ++i )
        {
            if( m_GaugeControllers[i].gauge < 100.0f )
            {
                continue;
            }

            if (index == -1 || 
                m_GaugeControllers[index].gauge < m_GaugeControllers[i].gauge)
            {
                index = i;
            }
        }

        return index;
    }

    public void OnTurn()
    {
        if( m_TurnIndex == -1)
        {
            return;
        }

        m_GaugeControllers[m_TurnIndex].ResetGauge();
        m_TurnIndex = -1;
    }
}
