using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeController : MonoBehaviour {

    /// <summary>
    /// トータル速度テキスト文言
    /// </summary>
    private static readonly string totalSpeedTextWord = "速度：{0}";

    /// <summary>
    /// ゲージテキスト文言
    /// </summary>
    private static readonly string gaugeTextWord = "ゲージ：{0}";

	/// <summary>
	/// 元速度入力フィールド
	/// </summary>
	[SerializeField]
	private InputField m_OriginSpeedInputField;
	public InputField originSpeedInputField
	{
		get{ return m_OriginSpeedInputField; }
	}

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
    /// ゲージバーイメージ
    /// </summary>
    [SerializeField]
    private Image m_BarImage;

	/// <summary>
	/// 元速度
	/// </summary>
	private int m_OriginSpeed;

    /// <summary>   
    /// トータル速度
    /// </summary>
    private int m_TotalSpeed;

    /// <summary>
    /// ゲージ量
    /// </summary>
    private float m_Gauge;
    public float gauge
    {
        get { return m_Gauge; }
    }

	/// <summary>
	/// ゲージ管理
	/// </summary>
	private GaugeManager m_GaugeManager;

	/// <summary>
	/// 初期化
	/// </summary>
	public void Initialize(GaugeManager manager )
	{
        // ゲージ管理取得
		m_GaugeManager = manager;

        // 元速度入力コールバック設定
        m_OriginSpeedInputField.onEndEdit.AddListener(OnOriginSpeedInput);
	}

    /// <summary>
    /// 元速度入力
    /// </summary>
    private void OnOriginSpeedInput(string value)
    {
        m_OriginSpeed = 0;

        int.TryParse(value, out m_OriginSpeed);

        CalcTotalSpeedAfterSetTotalSpeed();
    }

    public void OnStartSimulate()
    {
        m_OriginSpeedInputField.readOnly = true;
    }

    /// <summary>
    /// トータル速度算出後設定
    /// </summary>
	public void CalcTotalSpeedAfterSetTotalSpeed()
    {
        // トータル速度算出
        float total_speed = (float)m_OriginSpeed * (1.0f + (float)m_GaugeManager.GetFacilityRate() * 0.01f);

        // トータル速度設定
        SetTotalSpeed(Utility.RoundUpIntFromFloat(total_speed));
    }

	/// <summary>
	/// トータル速度設定
	/// </summary>
	/// <param name="speed">Speed.</param>
    public void SetTotalSpeed(int speed)
    {
        // テキスト設定
        m_TotalSpeedText.text = string.Format(GaugeController.totalSpeedTextWord, speed);

        // 速度設定
		m_TotalSpeed = speed;
    }
    
    /// <summary>
    /// レートからゲージ加算
    /// </summary>
    public void AddGaugeFromRate(float rate)
    {
		m_Gauge += (float)m_TotalSpeed * rate;

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
