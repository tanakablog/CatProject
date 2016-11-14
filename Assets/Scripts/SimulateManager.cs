using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimulateManager : MonoBehaviour {

	/// <summary>
	/// 施設効果入力
	/// </summary>
	[SerializeField]
	private InputField m_FacilityInputField;

	/// <summary>
	/// ゲージ管理
	/// </summary>
	[SerializeField]
	private GaugeManager m_GaugeManager;

	/// <summary>
	/// 施設レート
	/// </summary>
	private int m_FacilityRate;
	public int facilityRate
	{
		get{ return m_FacilityRate; }
	}

	void Awake()
	{
		m_FacilityInputField.onEndEdit.AddListener (OnFacilityInput);

        m_GaugeManager.Initialize(this);
	}

	public void OnFacilityInput( string input )
	{
		m_FacilityRate = 0;
		
		int.TryParse (input, out m_FacilityRate);

        // 計算結果を表示
        m_FacilityInputField.text = m_FacilityRate.ToString();

        m_GaugeManager.OnChangeFacilityRate();
	}

	/// <summary>
	/// シュミレート開始
	/// </summary>
	public void OnStartSimulate()
	{
        m_FacilityInputField.readOnly = true;

        m_GaugeManager.OnStartSimulate();
	}

}
