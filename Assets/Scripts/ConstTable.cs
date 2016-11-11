using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility {

	public static int RoundUpIntFromFloat(float value )
	{
		return (int)(value + 0.9f);
	}
}

public class ConstTable : MonoBehaviour {

    /// <summary>
    /// ゲージ更新時のレート
    /// </summary>
    public const float gaugeUpdateRate = 0.07f;

    /// <summary>
    /// ターン獲得時のバーの色
    /// </summary>
    public static readonly Color turnBarColor = Color.red;
}
