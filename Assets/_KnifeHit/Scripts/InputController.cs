using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

	[SerializeField] private GameObject knife;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(GameManager.instance.CurrentState == GameStates.Play)
		{
			if(Input.GetMouseButtonDown(0))
			{
				Instantiate(knife,transform.position,knife.transform.rotation);
			}
		}
	}
}
