using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodLogController : MonoBehaviour {

	[SerializeField] private float maxSpeed = 0;
	[SerializeField] private float speed = 0;
	[SerializeField] private int direction = 1;
	// Use this for initialization
	void Start () {
		StartCoroutine(PinPongSpeed());
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
			Debug.Log("Hit!!");
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
}
