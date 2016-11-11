using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeManager : MonoBehaviour {

    /// <summary>
    /// ゲージ制御配列
    /// </summary>
    [SerializeField]
    private GaugeController[] m_GaugeControllers;

    /// <summary>
    /// ターン獲得したインデックス
    /// </summary>
    private int m_TurnIndex;

	/// <summary>
	/// シュミレート管理
	/// </summary>
	private SimulateManager m_SimulateManager;

    private void Awake()
    {
        foreach (var gauge in m_GaugeControllers)
        {
            gauge.SetSpeed(123);
        }

        StartCoroutine(DoUpdateGauge());
    }

	/// <summary>
	/// 初期化
	/// </summary>
	public void Initialize(SimulateManager manager)
	{
		m_SimulateManager = manager;

		foreach (var gauge in m_GaugeControllers) {
			gauge.Initialize (this);
		}
	}

	/// <summary>
	/// 施設レート変更
	/// </summary>
	/// <param name="rate">Rate.</param>
	public void OnChangeFacilityRate( int rate )
	{
		foreach (var gauge in m_GaugeControllers) 
		{
		}
	}

	public void GetFacilityRate()
	{
		return m_SimulateManager.facilityRate;
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

    private IEnumerator DoUpdateGauge()
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

            // ターン獲得チェック
            CheckTurn();

            yield return null;
        }
    }

    /// <summary>
    /// ターン獲得チェック
    /// </summary>
    private void CheckTurn()
    {
        m_TurnIndex = -1;
        for (int i = 0; i < m_GaugeControllers.Length; ++i)
        {
            if (m_GaugeControllers[i].gauge < 100.0f)
            {
                continue;
            }

            if (m_TurnIndex == -1 ||
                m_GaugeControllers[m_TurnIndex].gauge < m_GaugeControllers[i].gauge)
            {
                m_TurnIndex = i;
            }
        }

        if (m_TurnIndex == -1)
        {
            return;
        }

        m_GaugeControllers[m_TurnIndex].OnTurn();
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
