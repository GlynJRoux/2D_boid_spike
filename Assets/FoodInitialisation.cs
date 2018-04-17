
using UnityEngine;

public class FoodInitialisation : MonoBehaviour {
	public GameObject[] foods;
	public GameObject foodPrefab, ballManager;
	[Range(1,500)]
	public int foodCounter = 200;

	int spawnRange;


	// Use this for initialization
	void Start()
	{
		spawnRange = ballManager.GetComponent<BallInitialisation>().spawnRange;
		foods = new GameObject[foodCounter];
		for (int i = 0; i < foodCounter; i++)
		{
			Vector3 foodSpawn = new Vector3(Random.Range(-spawnRange, +spawnRange),
				                    Random.Range(-spawnRange, +spawnRange),
				                    0);
			foods[i] = Instantiate(foodPrefab, this.transform.position + foodSpawn, Quaternion.identity) as GameObject;
		}
	}

	// Update is called once per frame
	void Update()
	{


	}



}