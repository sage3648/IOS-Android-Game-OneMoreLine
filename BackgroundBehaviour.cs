using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBehaviour : MonoBehaviour {
	private Vector3 _obstacleSize;
	private float _timer;

	// Use this for initialization
	void Start () {
		_timer = 0.3f;
		_obstacleSize = transform.localScale; 
	}
	
	// Update is called once per frame
	void Update () {
		_timer -= Time.deltaTime;
		if (_timer < 0f)
		{
			_timer = 0.3f;
			_obstacleSize.y += 5f;
			transform.localScale = _obstacleSize; 
		}
	}
}
