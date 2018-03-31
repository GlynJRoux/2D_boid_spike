using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballInitialisation : MonoBehaviour
{

    public GameObject[] objects;
    public GameObject circlePrefab;
    public int randomobjectCounter;
    public Vector3 spawnRangex = new Vector3(95, 95);
    public Vector3 spawnRangey = new Vector3(95, 95);

    public GameObject circle;

    // Use this for initialization
    void Start()
    {
        randomobjectCounter = Random.Range(1, 20);

        objects = new GameObject[randomobjectCounter];
        for (int i = 0; i < randomobjectCounter; i++)
        {
            Vector3 circlePosition = new Vector3(Random.Range(-spawnRangex.x, spawnRangex.x),
                                  Random.Range(-spawnRangey.y, spawnRangey.y),
                                  Random.Range(0, 0));
            objects[i] = Instantiate(circlePrefab, this.transform.position + circlePosition, Quaternion.identity) as GameObject;

        }


    }

    // Update is called once per frame
    void Update()
    {

    }
}