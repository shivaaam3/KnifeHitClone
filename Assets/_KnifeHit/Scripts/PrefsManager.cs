using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefsManager {
	#region Playerprefs
	private const string HIGH_SCORE = "highscore";
	private const string APPLES = "apples";
	#endregion


	#region Properties
	public static int HighScore
	{
		set{PlayerPrefs.SetInt(HIGH_SCORE,value);}
		get{return PlayerPrefs.GetInt(HIGH_SCORE);}
	}

	public static int Apples
	{
		set{PlayerPrefs.SetInt(APPLES,value);}
		get{return PlayerPrefs.GetInt(APPLES);}
	}
	#endregion
}
