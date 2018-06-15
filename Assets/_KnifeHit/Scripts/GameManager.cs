using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour {

	#region variables
	[SerializeField] private GameStates currentState;
	[SerializeField] private int currentLevelNumber = 0;

	private GameObject currentLevel;
	private float levelDelay = 1.0f;

	public static GameManager instance = null;
	public static Action gameOver, levelCleared;
	public int score = 0;
	#endregion


	#region Other scripts references
	public UIManager uimanager;
	public AudioManager audioManager;
	public PlayerController playerController;
	public WoodLogController woodLogController;
	#endregion

	public GameStates CurrentState
	{
		get {return currentState;}
	}


	#region Monobehaviour methods
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
	#endregion


	public void StartGame()
	{
		currentState = GameStates.Play;
		currentLevelNumber = 0;
		score = 0;

		DestroyOldLevel();
		currentLevel = Instantiate(Resources.Load(Constants.LEVEL_NUMBER[currentLevelNumber], typeof(GameObject))) as GameObject;
		woodLogController = GameObject.FindObjectOfType<WoodLogController>();
		levelCleared();

		uimanager.UpdateScore(GameManager.instance.score);
		uimanager.UpdateApples(PrefsManager.Apples);
		uimanager.UpdateKnives(playerController.knivesLimit);
	}

	public void PauseGame()
	{
		currentState = GameStates.Pause;
	}

	public void EndGame()
	{
		currentState = GameStates.Over;
		currentLevel.SetActive(false);

		if(score>=PrefsManager.HighScore)
		{
			PrefsManager.HighScore = score;
		}
		gameOver();
	}

	public void LevelCleared()
	{
		if(CurrentState == GameStates.Over)
			return;
		
		Debug.Log("Level Cleared");
		PauseGame();
		currentLevel.SetActive(false);
		if(currentLevelNumber == Constants.LEVEL_NUMBER.Length-1)
		{
			Debug.Log("Game Completed!!");
			uimanager.levelText.text = "GAME COMPLETED!!";
			uimanager.levelText.gameObject.SetActive(true);
			return;
		}
		++currentLevelNumber;

		if(currentLevelNumber % 5 == 4)
			uimanager.levelText.text = Constants.BOSS_LEVEL;
		else
			uimanager.levelText.text = "LEVEL " + (currentLevelNumber+1).ToString();
		uimanager.levelText.gameObject.SetActive(true);

		StartCoroutine(StartNewLevel());
	}

	IEnumerator StartNewLevel()
	{
		yield return new WaitForSeconds(levelDelay);
		DestroyOldLevel();

		currentLevel = Instantiate(Resources.Load(Constants.LEVEL_NUMBER[currentLevelNumber], typeof(GameObject))) as GameObject;
		woodLogController = GameObject.FindObjectOfType<WoodLogController>();
		levelCleared();
		currentState = GameStates.Play;
	}

	private void DestroyOldLevel()
	{
		if(currentLevel != null)
			Destroy(currentLevel);
	}
}
