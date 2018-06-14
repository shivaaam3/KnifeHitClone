using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PlayerController : MonoBehaviour {

	[SerializeField] private GameObject knife;
	private int knivesThrown = 0;
	public int knivesLimit = 0;
	public int knivesHit = 0;

	void OnEnable()
	{
		GameManager.levelCleared += ResetVariables;
	}

	void OnDisable()
	{
		GameManager.levelCleared -= ResetVariables;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(GameManager.instance.CurrentState == GameStates.Play && Input.GetMouseButtonDown(0))
		{
			if(knivesThrown<knivesLimit)
			{
				Instantiate(knife,transform.position,knife.transform.rotation);
				knivesThrown++;
				GameManager.instance.uimanager.UpdateKnives(knivesLimit - knivesThrown);
			}
		}
	}

	private void ResetVariables()
	{
		knivesThrown = 0;
		knivesHit = 0;
		knivesLimit = GameManager.instance.woodLogController.KnivesToClearLevel;
		GameManager.instance.uimanager.UpdateKnives(knivesLimit);
	}
}
