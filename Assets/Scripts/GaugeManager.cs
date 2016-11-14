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

    }

	/// <summary>
	/// 初期化
	/// </summary>
	public void Initialize(SimulateManager manager)
	{
        // シュミレート管理取得
		m_SimulateManager = manager;

        // ゲージ制御初期化
		foreach (var gauge in m_GaugeControllers) {
			gauge.Initialize (this);
		}
    }

    /// <summary>
    /// シュミレート開始
    /// </summary>
    public void OnStartSimulate()
    {
        foreach (var gauge in m_GaugeControllers)
        {
            gauge.OnStartSimulate();
        }

        // ゲージ更新開始
        StartCoroutine(DoUpdateGauge());
    }

	/// <summary>
	/// 施設レート変更
	/// </summary>
	/// <param name="rate">Rate.</param>
	public void OnChangeFacilityRate()
	{
		foreach (var gauge in m_GaugeControllers) 
		{
            // トータル速度設定
            gauge.CalcTotalSpeedAfterSetTotalSpeed();
		}
	}

    /// <summary>
    /// 施設レート取得
    /// </summary>
    /// <returns></returns>
	public int GetFacilityRate()
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

    /// <summary>
    /// ゲージ更新コルーチン
    /// </summary>
    /// <returns></returns>
    private IEnumerator DoUpdateGauge()
    {
        // ターンインデックス初期化
        m_TurnIndex = -1;

        int count = 0;

        while(true)
        {
            // ターン獲得チェック
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
