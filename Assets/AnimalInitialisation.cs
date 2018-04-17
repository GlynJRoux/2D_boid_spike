
using UnityEngine;

public class AnimalInitialisation : MonoBehaviour
{

	public GameObject[] birds;
	public GameObject birdPrefab, ballManager,foodManager;
	[Range(1,300)]
	public int birdCounter = 10;
	int spawnRange;

	[Range(0, 150)]
	public float birdSpeed = 7;

	[Range(1,50)]
	public float foodSight = 5f;

	[Range(0, 150)]
	public float neighbourDistance = 2f;

	[Range(1,1000)]
	public int energy = 500;

	[Range(1,50)]
	public int energyBoost = 10;

	// Use this for initialization
	void Start()
	{
		spawnRange = ballManager.GetComponent<BallInitialisation>().spawnRange;
		birds = new GameObject[birdCounter];
		for (int i = 0; i < birdCounter; i++)
		{
			Vector3 birdPosition = new Vector3(Random.Range(-spawnRange, +spawnRange),
				Random.Range(-spawnRange, +spawnRange),
				0);
			birds[i] = Instantiate(birdPrefab, this.transform.position + birdPosition, Quaternion.identity) as GameObject;
			birds[i].GetComponent<BirdBoids>().birdManager = this.gameObject;
			birds[i].GetComponent<BirdBoids>().ballManager = ballManager;
			birds[i].GetComponent<BirdBoids>().foodManager = foodManager;
		}
	}


	void Update()
	{
	}

}