using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeController : MonoBehaviour {

	[SerializeField] float magnitude;
	private Rigidbody2D rb;
	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody2D>();
		rb.AddForce(Vector2.up*magnitude);
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.tag == Strings.LOG && gameObject.tag == Strings.KNIFE)
		{
			gameObject.tag = Strings.HIT_KNIFE;
			transform.parent = coll.gameObject.transform;
			rb.velocity = Vector2.zero;
			rb.freezeRotation = true;
			rb.isKinematic = true;

			GameManager.instance.woodLogController.gameObject.GetComponent<Animator>().Play("LogShake");

			++GameManager.instance.playerController.knivesHit;
			GameManager.instance.uimanager.UpdateScore(++GameManager.instance.score);
			Debug.Log("Hit!!");

			if(GameManager.instance.playerController.knivesHit == GameManager.instance.playerController.knivesLimit)
			{
				GameManager.instance.LevelCleared();
			}
		}

		if(coll.tag == Strings.APPLE)
		{
			Destroy(coll.gameObject);
			GameManager.instance.uimanager.UpdateApples(++PrefsManager.Apples);
		}
	}

	void OnCollisionEnter2D(Collision2D coll) //GAMEOVER CHECK
	{
		if(coll.collider.tag == Strings.HIT_KNIFE)
		{
			GameManager.instance.woodLogController.gameObject.GetComponent<Animator>().Play("LogShake");
			GetComponent<BoxCollider2D>().enabled = false;
			rb.velocity = new Vector2(rb.velocity.x, 0);
			rb.AddForce(Vector2.down*magnitude);

			Destroy(gameObject,1f);
			Debug.Log("Game Over!!");
			GameManager.instance.EndGame();
		}
	}
}
