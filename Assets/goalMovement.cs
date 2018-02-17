using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script will be attached to the world
//Allows for the boids target to be moved

public class goalMovement : MonoBehaviour {

	public int movementSpeed = 1; //TODO Allow user to change this in settings to move faster

	private Vector3 moveDirection = Vector3.zero;

	public GameObject target = null;

	// Use this for initialization
	void Start () {
		Cursor.visible = true; 
	}

	// Update is called once per frame
	void Update () {
		float currentX = GetComponent<Camera>().transform.position.x;
		float currentZ = GetComponent<Camera>().transform.position.z;
		// Debug.Log("Tick");

		//Forward
		if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)){
			currentZ += (movementSpeed * Time.deltaTime);
		}

		//Down
		if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)){
			currentZ -= (movementSpeed * Time.deltaTime);
		}

		//Right
		if (Input.GetKey(KeyCode.RightArrow)  || Input.GetKey(KeyCode.D)){
			currentX += (movementSpeed * Time.deltaTime);
		}

		//Left
		if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)){
			currentX -= (movementSpeed * Time.deltaTime);
		}
		target.transform.position = new Vector3(currentX, target.transform.position.y, currentZ);
	}
}
