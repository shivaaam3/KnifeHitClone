using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefsManager {

	private const string HIGH_SCORE = "highscore";


	#region Properties

	public static int HighScore
	{
		set{PlayerPrefs.SetInt(HIGH_SCORE,value);}
		get{return PlayerPrefs.GetInt(HIGH_SCORE);}
	}

	#endregion
}
