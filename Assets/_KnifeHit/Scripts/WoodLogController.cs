using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodLogController : MonoBehaviour {

	[SerializeField] private MovementType currentMovmentType;
	[SerializeField] private float maxSpeed = 0;
	[SerializeField] private float speed = 0;
	[SerializeField] private int direction = 1;
	[SerializeField] private int knivesToClearLevel = 0;

	public int KnivesToClearLevel
	{
		get{return knivesToClearLevel;}
	}

		
	// Use this for initialization
	void Start () {
		GameManager.levelCleared();
		StartMotion();
	}
	
	// Update is called once per frame
	void Update () {
		if(GameManager.instance.CurrentState == GameStates.Play)
		{
			transform.Rotate(0,0,speed*direction*Time.deltaTime);
		}
	}
		

	private void StartMotion()
	{
		switch(currentMovmentType)
		{
			case MovementType.Rotate:
				speed = maxSpeed;
				break;
			case MovementType.PingpongRotate:
				StartCoroutine(PinPongSpeed());
				break;
			case MovementType.PingpongRotateReverse:
				StartCoroutine(PingPongReverse());
				break;
		}
	}

	IEnumerator PinPongSpeed()
	{
		while(GameManager.instance.CurrentState == GameStates.Play)
		{
			speed = Mathf.Lerp(0,maxSpeed,Mathf.PingPong(Time.time,2f));
			yield return null;
		}
	}

	IEnumerator PingPongReverse()
	{
		while(GameManager.instance.CurrentState == GameStates.Play)
		{
			speed = Mathf.Lerp(-maxSpeed,maxSpeed,Mathf.PingPong(Time.time,2f));
			yield return null;
		}
	}
}
