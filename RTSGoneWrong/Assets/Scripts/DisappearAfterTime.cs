using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearAfterTime : MonoBehaviour {

	private float timeToDisappear;
	private float timer;

	// Use this for initialization
	void Start() 
	{
		timeToDisappear = 8.0f;
		timer = 0;
	}
	
	// Update is called once per frame
	void Update() 
	{
		if (timer >= timeToDisappear)
		{
			Destroy(gameObject);
		}

		timer += Time.deltaTime;
	}
}
