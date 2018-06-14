using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

	[SerializeField] GameObject GameOverPanel;
	[SerializeField] float delay;
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
		yield return new WaitForSeconds(delay);
		GameOverPanel.SetActive(true);
	}
}
