using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class BarrierBehaviour : MonoBehaviour
{
	private Vector3 _obstacleSize;
	private float _timer;
	private Color _color; 
	// Use this for initialization
	void Start ()
	{
		_timer = 0.05f;
		_obstacleSize = transform.localScale; 
	}
	// Update is called once per frame
	void Update ()
	{
		_timer -= Time.deltaTime;
		if (Input.GetMouseButtonDown(0))
		{
			_color = gameObject.GetComponent<SpriteRenderer>().color; 
		}
		if (Input.GetMouseButton(0))
		{
			gameObject.GetComponent<BoxCollider2D>().enabled = false; 
			gameObject.GetComponent<SpriteRenderer>().color = new Color32(2,255,255,255);
                                                    //new Color(72f,73f,75f);
		}
		if (Input.GetMouseButtonUp(0))
		{
			gameObject.GetComponent<BoxCollider2D>().enabled = true; 
			gameObject.GetComponent<SpriteRenderer>().color = _color; 
		}


		if (_timer < 0f)
		{
			_timer = 0.3f;
			_obstacleSize.y += 7f;
			transform.localScale = _obstacleSize; 
		}
	}
	private void OnBecameInvisible()
	{
		//Destroy(gameObject);
	}
}
