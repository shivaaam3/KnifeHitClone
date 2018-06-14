using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodLogController : MonoBehaviour {

	[SerializeField] private MovementType currentMovmentType;
	[SerializeField] private float maxSpeed = 0;
	[SerializeField] private float speed = 0;
	[SerializeField] private int direction = 1;
	[SerializeField] private int knivesToClearLevel = 0;
	[SerializeField] private int knivesHit = 0;

	public int KnivesToClearLevel
	{
		get{return knivesToClearLevel;}
	}

	public int KnivesHit
	{
		get{return knivesHit;}
	}

	void OnEnable()
	{
		GameManager.gamePlay += StartMotion;	
	}

	void OnDisable()
	{
		GameManager.gamePlay -= StartMotion;	
	}
	// Use this for initialization
	void Start () {
		GameManager.levelCleared();
	}
	
	// Update is called once per frame
	void Update () {
		if(GameManager.instance.CurrentState == GameStates.Play)
		{
			transform.Rotate(0,0,speed*direction*Time.deltaTime);
		}
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.tag == "Knife")
		{
			coll.tag = "HitKnife";
			coll.transform.parent = transform;
			coll.attachedRigidbody.velocity = Vector2.zero;
			coll.attachedRigidbody.freezeRotation = true;
			coll.attachedRigidbody.isKinematic = true;

			GetComponent<Animator>().Play("LogShake");

			++knivesHit;
			Debug.Log("Hit!!");

			if(KnivesHit == KnivesToClearLevel)
			{
				GameManager.instance.LevelCleared();
			}
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
