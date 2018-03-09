using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodInitialisation : MonoBehaviour
{
    public GameObject[] allFood;
    public GameObject foodPrefab;
    public Vector3 spawnRange = new Vector3(18, 18);
    public int foodCounter = 1;
    public Vector3 seenRange = new Vector3(5, 5, 5);
    public float spawnDelay = 4f;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("SpawnFood", spawnDelay, spawnDelay);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void SpawnFood()
    {
        for (int i = 0; i < foodCounter; i++)
        {
            Vector3 foodPosition = new Vector3(Random.Range(-spawnRange.x, spawnRange.x),
                                  Random.Range(-spawnRange.y, spawnRange.y),
                                  Random.Range(0, 0));
            allFood[i] = Instantiate(foodPrefab, this.transform.position + foodPosition, Quaternion.identity) as GameObject;

        }

    }


}
