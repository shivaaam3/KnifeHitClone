using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	[SerializeField] private List<AudioClip> audioclips;
	[SerializeField] private AudioSource soundsAudioSource;
	// Use this for initialization
	void Start () {
		
	}
	
	public void PlayClip(int clipindex)
	{
		soundsAudioSource.PlayOneShot(audioclips[clipindex]);
	}
}
