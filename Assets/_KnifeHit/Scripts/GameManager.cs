using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour {

	[SerializeField] private GameStates currentState;
	[SerializeField] private int currentLevelNumber = 0;

	private GameObject currentLevel;

	public static GameManager instance = null;
	public static Action gameOver, gamePlay, levelCleared;
	public int score = 0;

	#region Other scripts references
	public UIManager uimanager;
	#endregion

	public GameStates CurrentState
	{
		get {return currentState;}
	}

	void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
		else if(instance != this)
		{
			Destroy(this.gameObject);
		}
	}
	// Use this for initialization
	void Start () {
		
		currentState = GameStates.Over;
	}
	

	private void StartGame()
	{
		currentState = GameStates.Play;
		currentLevelNumber = 0;
		score = 0;
		uimanager.UpdateScore(GameManager.instance.score);
		currentLevel = Instantiate(Resources.Load(Strings.LEVEL_NUMBER[currentLevelNumber], typeof(GameObject))) as GameObject;
		levelCleared();
	}

	internal void PauseGame()
	{
		currentState = GameStates.Pause;
	}

	internal void EndGame()
	{
		currentState = GameStates.Over;
		gameOver();

		Destroy(currentLevel);

		if(score>=PrefsManager.HighScore)
		{
			PrefsManager.HighScore = score;
		}
		Debug.Log("Score: " + score +" "+ "HighScore: " + PrefsManager.HighScore);
	}

	public void LevelCleared()
	{
		Debug.Log("Level Cleared");
		if(currentLevelNumber == Strings.LEVEL_NUMBER.Length-1)
		{
			Debug.Log("Game Completed!!");
			return;
		}
		GameObject oldLevel = currentLevel;
		Destroy(oldLevel);
		currentLevel = Instantiate(Resources.Load(Strings.LEVEL_NUMBER[++currentLevelNumber], typeof(GameObject))) as GameObject;
		levelCleared();
	}
}
