using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeController : MonoBehaviour {

	[SerializeField] float magnitude;

	// Use this for initialization
	void Start () {

		GetComponent<Rigidbody2D>().AddForce(Vector2.up*magnitude);
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if(tag == "Knife" && coll.collider.tag == "HitKnife")
		{
			GetComponent<BoxCollider2D>().enabled = false;
			Rigidbody2D rb = GetComponent<Rigidbody2D>();
			rb.velocity = new Vector2(rb.velocity.x, 0);
			rb.AddForce(Vector2.down*magnitude);
			Destroy(gameObject,1f);
		}
	}
}
