using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	
	public GameObject Player;
	private Vector3 _cameraPos;
	private float _cameraStartPosX;
	private Vector3 _cameraStartPosition;
	private Vector3 _movement; 
	

	// Use this for initialization
	void Start ()
	{
		_cameraStartPosition = transform.position; 
		_cameraStartPosX = transform.position.x; 
		_cameraPos.y = transform.position.y;
		_cameraPos.z = -10f; 
	}
	
	// Update is called once per frame
	void Update ()
	{
	
		if (gameObject.GetComponent<ScoreTracker>().GameOver == true)
		{
			_movement = Vector3.Lerp(gameObject.transform.position, new Vector3(0f,0f,-10f), Time.fixedDeltaTime);
			gameObject.transform.position = _movement; 
		}
		
		_cameraPos.y = Player.transform.position.y + 3f;
		_cameraPos.x = Player.transform.position.x; 
		transform.position = _cameraPos;
	
	}
}
