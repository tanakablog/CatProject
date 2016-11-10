using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeController : MonoBehaviour {
    /// <summary>
    /// ゲージバーイメージ
    /// </summary>
    [SerializeField]
    private Image m_BarImage;

    /// <summary>
    /// 速度
    /// </summary>
    private int m_Speed;
    public int speed
    {
        get { return m_Speed; }
        set { m_Speed = value; }
    }

    /// <summary>
    /// ゲージ
    /// </summary>
    private float m_Gauge;
    public float gauge
    {
        get { return m_Gauge; }
    }


    /// <summary>
    /// レートからゲージ加算
    /// </summary>
    public void AddGaugeFromRate(float rate)
    {
        m_Gauge += (float)m_Speed * rate;

        UpdateGaugeBar();
    }

    /// <summary>
    /// ゲージリセット
    /// </summary>
    public void ResetGauge()
    {
        m_Gauge = 0.0f;

        UpdateGaugeBar();
    }

    /// <summary>
    /// ゲージバー更新
    /// </summary>
    private void UpdateGaugeBar()
    {
        m_BarImage.fillAmount = Mathf.Clamp(m_Gauge * 0.01f, 0.0f, 1.0f);
    }
}
