using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodInitialisation : MonoBehaviour
{
    public GameObject food;
    public static GameObject[] foods;
    public GameObject foodPrefab;
    public Vector3 spawnRangex = new Vector3(95, 95);
    public Vector3 spawnRangey = new Vector3(95, 95);
    public Vector3 foodSpawn;
    public static int foodCounter = 200;


    // Use this for initialization
    void Start()
    {
        foods = new GameObject[foodCounter];
        for (int i = 0; i < foodCounter; i++)
        {


            foodSpawn = new Vector3(Random.Range(-spawnRangex.x, spawnRangex.x),
                                   Random.Range(-spawnRangey.y, spawnRangey.y),
                                   Random.Range(0, 0));
            foods[i] = Instantiate(foodPrefab, this.transform.position + foodSpawn, Quaternion.identity) as GameObject;
            foods[i].GetComponent<birdFood>().foodManager = this.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {


    }



}




