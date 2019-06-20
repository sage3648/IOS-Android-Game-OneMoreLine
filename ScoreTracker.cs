using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour
{
	public Text ScoreTxt;  
	public float Score;
	public GameObject Player; 
	private Vector2 _playerPos;
	private float _highestScore; 
	public Canvas GameOverCanvas;
	public bool GameOver;
	public Button Restart;
	public Text RecentScore, BestScore, AverageScore, TotalScore;
	public bool GameRunning;

	private bool _changedAverage;
	private bool _changedTotal; 
	
	// Use this for initialization
	void Start ()
	{
		_changedAverage = false;
		_changedTotal = false; 
		//PlayerPrefs.SetFloat("Total", 0); 
		//PlayerPrefs.SetFloat("Average", 0); 
		//PlayerPrefs.DeleteAll();
		

		
		Restart.onClick.AddListener(NewGame);
		_playerPos = Player.transform.position;
		Score = 0f;
		ScoreTxt.text = "";
		//GameOverCanvas.GetComponent<Canvas>().enabled = false; 
		GameOverCanvas.GetComponent<Canvas>().transform.position = new Vector2(50f,50f);
		if (PlayerPrefs.GetFloat("HighestScore") < 0)
		{
			PlayerPrefs.SetFloat("HighestScore", 0); 
		}
		if (PlayerPrefs.GetFloat("Average") < 0)
		{
			PlayerPrefs.SetFloat("Average", 0); 
		}
		if (PlayerPrefs.GetFloat("Total") < 0)
		{
			PlayerPrefs.SetFloat("Total", 0); 
		}
	}

	void NewGame()
	{
		SceneManager.LoadScene("Main");
	}
	// Update is called once per frame
	void Update ()
	{
		if (GameOver == true)
		{
			/*var tries = PlayerPrefs.GetFloat("Tries");
			tries++; 
			PlayerPrefs.SetFloat("Tries", tries);
			PlayerPrefs.SetFloat("Total", _highestScore += PlayerPrefs.GetFloat("Total"));
			var average = PlayerPrefs.GetFloat("Total") / tries; 
			PlayerPrefs.SetFloat("Average", average);
			PlayerPrefs.SetFloat("HighScore", _highestScore);
			ScoreTxt.text = ""; */
			//GameOverCanvas.GetComponent<Canvas>().enabled = false; 

			if (_changedAverage == false)
			{
				
				var tries = PlayerPrefs.GetFloat("Tries");
				tries += 1f;
				var average = PlayerPrefs.GetFloat("Total") / tries; 
				PlayerPrefs.SetFloat("Average",average);
				_changedAverage = true; 
			}
			
			

			if (_highestScore > PlayerPrefs.GetFloat("HighestScore"))
			{
				PlayerPrefs.SetFloat("HighestScore", _highestScore);
			}
			
			if (_highestScore < PlayerPrefs.GetFloat("Total") && _changedTotal == false)
			{
				float newTotal = PlayerPrefs.GetFloat("Total");
				newTotal += _highestScore; 
				PlayerPrefs.SetFloat("Total", newTotal);
				_changedTotal = true; 
			}
			
			GameOverCanvas.GetComponent<Canvas>().transform.position = new Vector2(0f,0f);
			RecentScore.text = "" + (int)_highestScore;
			
			BestScore.text = "BEST    " + (int)PlayerPrefs.GetFloat("HighestScore");
			AverageScore.text = "AVERAGE    " + (int) PlayerPrefs.GetFloat("Average");
			TotalScore.text = "TOTAL    " + (int) PlayerPrefs.GetFloat("Total"); 
		}
		_playerPos.y = Player.transform.position.y;
		Score = _playerPos.y; 
		
		
		if (Score > _highestScore)
		{
			_highestScore = Score;
			int score = (int)_highestScore;
			ScoreTxt.text = "" + score;
		}
	}
}
