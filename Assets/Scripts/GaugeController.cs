using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeController : MonoBehaviour {

    /// <summary>
    /// 速度テキスト文言
    /// </summary>
    private static readonly string speedTextWord = "速度：{0}";

    /// <summary>
    /// ゲージテキスト文言
    /// </summary>
    private static readonly string gaugeTextWord = "ゲージ：{0}";

    /// <summary>
    /// ゲージバーイメージ
    /// </summary>
    [SerializeField]
    private Image m_BarImage;

    /// <summary>
    /// 速度テキスト
    /// </summary>
    [SerializeField]
    private Text m_TotalSpeedText;

    /// <summary>
    /// ゲージテキスト
    /// </summary>
    [SerializeField]
    private Text m_GaugeText;

    /// <summary>   
    /// 速度
    /// </summary>
    private int m_Speed;

    /// <summary>
    /// ゲージ量
    /// </summary>
    private float m_Gauge;
    public float gauge
    {
        get { return m_Gauge; }
    }

    public void SetSpeed(int speed)
    {
        m_TotalSpeedText.text = string.Format(GaugeController.speedTextWord, speed);

        m_Speed = speed;
    }


    /// <summary>
    /// レートからゲージ加算
    /// </summary>
    public void AddGaugeFromRate(float rate)
    {
        m_Gauge += (float)m_Speed * rate;

        UpdateGauge();
    }

    /// <summary>
    /// ゲージリセット
    /// </summary>
    public void ResetGauge()
    {
        m_Gauge = 0.0f;

        m_BarImage.color = Color.white;

        UpdateGauge();
    }

    /// <summary>
    /// ターン獲得
    /// </summary>
    public void OnTurn()
    {
        m_BarImage.color = ConstTable.turnBarColor;
    }

    /// <summary>
    /// ゲージ更新
    /// </summary>
    private void UpdateGauge()
    {
        m_BarImage.fillAmount = Mathf.Clamp(m_Gauge * 0.01f, 0.0f, 1.0f);

        m_GaugeText.text = string.Format(GaugeController.gaugeTextWord, m_Gauge);
    }
}
