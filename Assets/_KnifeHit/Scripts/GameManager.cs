using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour {

	[SerializeField] private GameStates currentState;
	[SerializeField] private int currentLevelNumber = 0;

	private GameObject currentLevel;
	private float levelDelay = 1.0f;

	public static GameManager instance = null;
	public static Action gameOver, levelCleared;
	public int score = 0;

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

		DestroyOldLevel();
		currentLevel = Instantiate(Resources.Load(Strings.LEVEL_NUMBER[currentLevelNumber], typeof(GameObject))) as GameObject;
		woodLogController = GameObject.FindObjectOfType<WoodLogController>();
		levelCleared();

		uimanager.UpdateScore(GameManager.instance.score);
		uimanager.UpdateApples(PrefsManager.Apples);
		uimanager.UpdateKnives(playerController.knivesLimit);
	}

	internal void PauseGame()
	{
		currentState = GameStates.Pause;
	}

	internal void EndGame()
	{
		currentState = GameStates.Over;

		DestroyOldLevel();

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
		if(currentLevelNumber == Strings.LEVEL_NUMBER.Length-1)
		{
			Debug.Log("Game Completed!!");
			uimanager.levelText.text = "GAME COMPLETED!!";
			uimanager.levelText.gameObject.SetActive(true);
			return;
		}
		++currentLevelNumber;


		if(currentLevelNumber % 5 == 4)
			uimanager.levelText.text = Strings.BOSS_LEVEL;
		else
			uimanager.levelText.text = "LEVEL " + (currentLevelNumber+1).ToString();

		uimanager.levelText.gameObject.SetActive(true);

		StartCoroutine(StartNewLevel());
	}

	IEnumerator StartNewLevel()
	{
		yield return new WaitForSeconds(levelDelay);
		DestroyOldLevel();
		currentState = GameStates.Play;
		currentLevel = Instantiate(Resources.Load(Strings.LEVEL_NUMBER[currentLevelNumber], typeof(GameObject))) as GameObject;
		woodLogController = GameObject.FindObjectOfType<WoodLogController>();
		levelCleared();
	}

	private void DestroyOldLevel()
	{
		if(currentLevel != null)
			Destroy(currentLevel);

		else
			Debug.Log("null level!");
	}
}
