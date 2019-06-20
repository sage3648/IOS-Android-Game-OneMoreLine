using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityScript.Macros;
using Random = UnityEngine.Random;

public class ObstacleGenerator : MonoBehaviour
{
	public Sprite obstacle1, obstacle2, obstacle3, obstacle4;
	private Vector2 _spawnPos;
	public GameObject Player; 
	private float _timer;
	private int _score;
	private int _scoreDeviation; 
	public GameObject Obstacle;
	private float _sizeIncreaser; 
	// Use this for initialization
	void Start ()
	{
		_timer = 1.5f; 
		_sizeIncreaser = 0.5f; 
	}

	// Update is called once per frame
	void Update ()
	{
		_spawnPos.y = Player.transform.position.y + 10f;
		_spawnPos.x = Random.Range(-2f, 2f); 
		_timer -= Time.deltaTime;
		_score = (int)gameObject.GetComponent<ScoreTracker>().Score;

		if (_score >= _scoreDeviation)
		{
			_sizeIncreaser = _score / 40; 
			if(_sizeIncreaser >= 1.5f)
			{
				_sizeIncreaser = 1.5f; 
			}
			_scoreDeviation += Random.Range(4, 6); 
			var newObstacle = Instantiate(Obstacle, _spawnPos, transform.rotation);
			float randomSize = Random.Range(0.05f, _sizeIncreaser);
			Vector3 newSize;
			newSize.x = randomSize;
			newSize.y = randomSize;
			newSize.z = 0f; 
			newObstacle.transform.localScale += newSize;
			int random = Random.Range(1, 5);
			int randomColour = Random.Range(1, 6);
			switch (randomColour)
			{
				case 1:
					newObstacle.GetComponent<SpriteRenderer>().color = new Color(0.16f, 0.5f, 0.9f);				
					break; 
				case 2:
					newObstacle.GetComponent<SpriteRenderer>().color = new Color(0.9f,0.5f, 0.13f);

					break;
				case 3:
					newObstacle.GetComponent<SpriteRenderer>().color = new Color(0.6f ,0.22f, 0.9f);
					break; 
				case 4:
					newObstacle.GetComponent<SpriteRenderer>().color = new Color(0.19f ,0.62f, 0f);
					break;
				case 5:
					newObstacle.GetComponent<SpriteRenderer>().color = new Color(1f ,0f, 0.6f);
					break; 
					
			}
			switch (random)
			{
				case 1:
					newObstacle.GetComponent<SpriteRenderer>().sprite = obstacle1;
					break; 
				case 2:
					newObstacle.GetComponent<SpriteRenderer>().sprite = obstacle2;
					break;
				case 3:
					newObstacle.GetComponent<SpriteRenderer>().sprite = obstacle3;
					break; 
				case 4:
					newObstacle.GetComponent<SpriteRenderer>().sprite = obstacle4;
					break; 
			}
			//_timer = Random.Range(1.5f, 2f); 
		}
	}

	private void OnBecameInvisible()
	{
		//Destroy(gameObject);
	}
}
