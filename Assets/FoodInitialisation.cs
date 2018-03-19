using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodInitialisation : MonoBehaviour
{
    public GameObject[] allFood;
    public GameObject foodPrefab;
    public int foodCounter = 10;
    public Vector3 spawnRangex = new Vector3(95, 95);
    public Vector3 spawnRangey = new Vector3(95, 95);

    // Use this for initialization
    void Start(){
        allFood = new GameObject[foodCounter];
        for (int i = 0; i < foodCounter; i++)
        {
            Vector3 foodPosition = new Vector3(Random.Range(-spawnRangex.x, spawnRangex.x),
                                  Random.Range(-spawnRangey.y, spawnRangey.y),
                                  Random.Range(0, 0));
            allFood[i] = Instantiate(foodPrefab, this.transform.position + foodPosition, Quaternion.identity) as GameObject;
            allFood[i].GetComponent<birdFood>().foodManager = this.gameObject;

        }
    }

    // Update is called once per frame
    void Update(){

    }
   


}
