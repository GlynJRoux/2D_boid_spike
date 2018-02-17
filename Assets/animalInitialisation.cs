using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animalInitialisation : MonoBehaviour {

	public GameObject[] birds;
	public GameObject birdPrefab;
	public int birdCounter = 10;
	public Vector3 spawnRange = new Vector3(5, 5, 5);

	[Range(0,150)]
	public int neighbourDistance = 25;

	[Range (0,10)]
	public float maxForce = 1f;

	[Range (0,2)]
	public float maxVelocity = 3f;

	// Use this for initialization
	void Start () {
		birds = new GameObject[birdCounter];
		for(int i = 0; i < birdCounter; i++)
		{
			Vector3 birdPosition = new Vector3 (Random.Range (-spawnRange.x, spawnRange.x),
				                  Random.Range (-spawnRange.y, spawnRange.y),
				                  Random.Range (0, 0));
			birds [i] = Instantiate (birdPrefab, this.transform.position + birdPosition, Quaternion.identity) as GameObject;
			birds [i].GetComponent<boid> ().boidManager = this.gameObject;

		}
	}
}