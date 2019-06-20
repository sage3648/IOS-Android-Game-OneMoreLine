using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityScript.Macros;
using Random = UnityEngine.Random;

public class PlayerMovement : MonoBehaviour
{
	private bool _gameStarted; 
	private bool _hooking;
	private int _colourPicker;
	public Button StartButton; 
	public GameObject Explosion;
	public GameObject Camera;
	public Text StartTxt; 
	private GameObject attachedObject = null;
	private Color _originalColour;
	private Vector2 _velocity;
	public bool GameOver;


	// Use this for initialization

	void Start()
	{
		StartButton.onClick.AddListener(StartGame);
		gameObject.AddComponent<LineRenderer>();
		_hooking = false;
		gameObject.GetComponent<SpriteRenderer>().enabled = false; 
	}

	void StartGame()
	{
		gameObject.GetComponent<SpriteRenderer>().enabled = true; 
		gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up*2, ForceMode2D.Impulse);
		_velocity = gameObject.GetComponent<Rigidbody2D>().velocity; 
		StartButton.enabled = false;
		StartTxt.GetComponent<Text>().text = "";
		_gameStarted = true; 
	}

	// Update is called once per frame
	void Update()
	{
		gameObject.GetComponent<Rigidbody2D>().velocity =
			gameObject.GetComponent<Rigidbody2D>().velocity.normalized * 8f; 
		
		GameObject closestObstacle = FindClosestObstacle("obstacle");


;		if (Input.GetMouseButtonDown(0) && _gameStarted == true)
		{
			gameObject.GetComponent<LineRenderer>().enabled = true; 
			_colourPicker = Random.Range(0, 4);
		
			attachedObject = closestObstacle;
			attachedObject.AddComponent<DistanceJoint2D>();
			attachedObject.GetComponent<DistanceJoint2D>().connectedBody = gameObject.GetComponent<Rigidbody2D>();

			_originalColour = attachedObject.GetComponent<SpriteRenderer>().color; 
			
			
			_hooking = true;

			if (transform.position.x < attachedObject.transform.position.x && transform.position.y < attachedObject.transform.position.y)
			{
				gameObject.GetComponent<SpriteRenderer>().flipY = false; 
				gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 3,ForceMode2D.Impulse);
			}

			if (transform.position.x > attachedObject.transform.position.x && transform.position.y < attachedObject.transform.position.y)
			{
				gameObject.GetComponent<SpriteRenderer>().flipY = true; 
				gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 3,ForceMode2D.Impulse);
			}


		}
		if (Input.GetMouseButtonUp(0))
		{
			attachedObject.GetComponent<SpriteRenderer>().color = Color.white; 
			gameObject.GetComponent<LineRenderer>().enabled = false; 
			Destroy(attachedObject.GetComponent<DistanceJoint2D>());
			_hooking = false;
			//Destroy(gameObject.GetComponent<DistanceJoint2D>());
		}
		if (Input.GetMouseButton(0))
		{

			attachedObject.GetComponent<SpriteRenderer>().color =
			Color.Lerp(_originalColour, Color.white, Mathf.PingPong(Time.time, 1));
			LineRenderer lr = gameObject.GetComponent<LineRenderer>();
			
			
			lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
			lr.SetWidth(0.02f, 0.02f);
			switch (_colourPicker)
			{
				case 1:
					lr.startColor = Color.cyan;
					break;
				case 2:
					lr.startColor = Color.magenta;
					break;
				case 3:
					lr.startColor = Color.yellow;
					break; 
			}
			lr.SetPosition(0,transform.position);
			lr.SetPosition(1, attachedObject.transform.position);
			
			//face direction of movement, sort of
			transform.right = attachedObject.transform.position - transform.position;
			if (transform.position.x > attachedObject.transform.position.x)
			{
				attachedObject.transform.Rotate(0,0,90 * Time.deltaTime);

			}

			if (transform.position.x < attachedObject.transform.position.x)
			{
				attachedObject.transform.Rotate(0,0,-90 * Time.deltaTime);
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "ScoreLine(Clone)")
		{
			Camera.GetComponent<ScoreTracker>().Score++; 
			Destroy(other.gameObject);
		}	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		Camera.GetComponent<ScoreTracker>().GameOver = true; 
		Instantiate(Explosion, gameObject.transform.position, transform.rotation);
		if (other.gameObject.name == "Barrier(Clone)")
		{
			if (_hooking == false)
			{
				Destroy(gameObject);
			}	
		}
		if (other.gameObject.name == "Obstacle(Clone)")
		{
			Destroy(gameObject);
		}
	}

	public GameObject FindClosestObstacle(String tag)
	{
		GameObject[] obstacles;
		GameObject closestObstacle = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		obstacles = GameObject.FindGameObjectsWithTag(tag);
		foreach (GameObject o in obstacles)
		{
			Vector3 difference = o.transform.position - position;
			float curDistance = difference.sqrMagnitude;
			if (curDistance < distance)
			{
				closestObstacle = o;
				distance = curDistance; 
			}
		}
		return closestObstacle; 
	}

}
