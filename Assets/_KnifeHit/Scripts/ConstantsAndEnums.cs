#region Gamestates
public enum GameStates
{
	Play,
	Pause,
	Over
}
#endregion

#region Wheel movement types
public enum MovementType
{
	Rotate,
	PingpongRotate,
	PingpongRotateReverse
}
#endregion

#region AudioClips
public enum Sounds
{
	WoodCut,
	KnifeClunk
}
#endregion

#region Constant strings
class Constants
{
	#region Level names
	public static string[] LEVEL_NUMBER = new string[] 
	{
		"Level1",
		"Level2",
		"Level3",
		"Level4",
		"Level5",
		"Level6",
		"Level7",
		"Level8",
		"Level9",
		"Level10"
	};
	#endregion

	#region Tags
	public static string APPLE = "Apple";
	public static string LOG = "Log";
	public static string KNIFE = "Knife";
	public static string HIT_KNIFE = "HitKnife";
	#endregion

	public static string BOSS_LEVEL = "Boss Level!";

}
#endregion
