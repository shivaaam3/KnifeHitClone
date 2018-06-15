using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	#region UI variables
	[SerializeField] GameObject GameOverPanel;
	[SerializeField] float delay;
	public Text scoreText;
	public Text appleText;
	public Text knivesText;
	public Text levelText;
	public Text gameOverScoreText;
	#endregion


	void OnEnable()
	{
		GameManager.gameOver += ShowGameOverPanel;	
	}

	void OnDisable()
	{
		GameManager.gameOver -= ShowGameOverPanel;	
	}
	// Use this for initialization
	void Start () {
		
	}


	private void ShowGameOverPanel()
	{
		StartCoroutine(GameOver());
	}

	IEnumerator GameOver()
	{
		gameOverScoreText.text = "SCORE: " + GameManager.instance.score +"\n"+"HIGHSCORE: " + PrefsManager.HighScore;
		yield return new WaitForSeconds(delay);
		GameOverPanel.SetActive(true);
	}

	public void UpdateScore(int score)
	{
		scoreText.text = "SCORE: " + score.ToString();
	}

	public void UpdateApples(int apples)
	{
		appleText.text = "APPLES: " + apples.ToString();
	}

	public void UpdateKnives(int knives)
	{
		knivesText.text = "KNIVES: " + knives.ToString();
	}
}
