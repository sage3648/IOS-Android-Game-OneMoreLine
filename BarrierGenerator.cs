using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarrierGenerator : MonoBehaviour
{

	public Button StartButton; 
	public GameObject Barrier;
	public GameObject Player;
	private Vector2 _barrierLeftPos;
	private Vector2 _barrierRightPos;
	private float _timer = 0f; 

	// Use this for initialization
	void Start ()
	{
		StartButton.onClick.AddListener(StartGame);
	}
	void StartGame()
	{
		_barrierLeftPos.x = -2.27f;
		_barrierLeftPos.y = -0.237f;
		_barrierRightPos.x = 2.28f;
		_barrierRightPos.y = -0.237f; 
		Instantiate(Barrier, _barrierLeftPos, transform.rotation);
		Instantiate(Barrier, _barrierRightPos, transform.rotation);
		_barrierLeftPos.x = -2.5f;
		Instantiate(Barrier, _barrierLeftPos, transform.rotation);
		_barrierRightPos.x = 2.5f;
		Instantiate(Barrier, _barrierRightPos, transform.rotation);
	}
	
	// Update is called once per frame
	void Update ()
	{
		_timer -= Time.deltaTime;
		if (_timer <= 0f)
		{
			_barrierLeftPos.y += 4f;
			_barrierRightPos.y += 4f; 
			_timer = 0.6f;
			//transform.localScale.y += 3f; 
			//Instantiate(Barrier, _barrierLeftPos, transform.rotation);
			//Instantiate(Barrier, _barrierRightPos, transform.rotation);
		}

	}
}
