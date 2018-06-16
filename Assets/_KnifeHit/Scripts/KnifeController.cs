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
		if(coll.tag == Constants.HIT_KNIFE)  //GAMEOVER CHECK
		{
			gameObject.tag = Constants.HIT_KNIFE;
			gameObject.GetComponent<BoxCollider2D>().enabled = false;
			rb.velocity = new Vector2(Random.Range(-10,10),-rb.velocity.y);
			GameManager.instance.EndGame();
			GameManager.instance.woodLogController.gameObject.GetComponent<Animator>().Play("LogShake");
			GameManager.instance.audioManager.PlayClip((int)Sounds.KnifeClunk);
			Handheld.Vibrate();

			Destroy(gameObject,1f);
			Debug.Log("Game Over!!");
		}

		if(gameObject.tag == Constants.KNIFE && coll.tag == Constants.LOG)
		{
			rb.velocity = Vector2.zero;
			rb.freezeRotation = true;
			rb.isKinematic = true;
			gameObject.tag = Constants.HIT_KNIFE;
			transform.parent = coll.gameObject.transform;

			GameManager.instance.audioManager.PlayClip((int)Sounds.WoodCut);
			GameManager.instance.woodLogController.gameObject.GetComponent<Animator>().Play("LogShake");

			GameManager.instance.playerController.knivesHit++;
			GameManager.instance.uimanager.UpdateScore(++GameManager.instance.score);

			GameManager.instance.uimanager.UpdateKnives(GameManager.instance.playerController.knivesLimit - GameManager.instance.playerController.knivesThrown);

			if(GameManager.instance.playerController.knivesHit == GameManager.instance.playerController.knivesLimit)
			{
				GameManager.instance.LevelCleared();
			}
		}

		if(coll.tag == Constants.APPLE)
		{
			coll.gameObject.SetActive(false);
			GameManager.instance.uimanager.UpdateApples(++PrefsManager.Apples);
		}
	}

}
