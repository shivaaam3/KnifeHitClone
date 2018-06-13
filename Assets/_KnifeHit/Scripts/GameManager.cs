using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour {

	[SerializeField] private GameStates currentState;
	public static GameManager instance = null;
	public static Action gameOver, gamePaused, gamePlay;
	public GameStates CurrentState
	{
		get {return currentState;}
	}

	void Awake()
	{
		if(instance == null)
		{
			instance = this;
			DontDestroyOnLoad(this.gameObject);
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
	
	// Update is called once per frame
	void Update () {
		
	}
}
