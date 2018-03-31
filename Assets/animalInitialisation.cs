using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animalInitialisation : MonoBehaviour
{

    public static GameObject[] birds;
    public GameObject birdPrefab;
    public int birdCounter = 10;
    public Vector3 spawnRangex = new Vector3(95, 95);
    public Vector3 spawnRangey = new Vector3(95, 95);
    public Vector3 averageBirdLocation;

    [Range(0, 150)]
    public static float birdSpeed = 7;

    [Range(0, 150)]
    public double neighbourDistance = 2;

    [Range(0, 10)]
    public float maxForce = 1f;

    [Range(0, 100)]
    public float maxVelocity = 3f;

    // Use this for initialization
    void Start()
    {
        birds = new GameObject[birdCounter];
        for (int i = 0; i < birdCounter; i++)
        {
            Vector3 birdPosition = new Vector3(Random.Range(-spawnRangex.x, spawnRangex.x),
                                  Random.Range(-spawnRangey.y, spawnRangey.y),
                                  Random.Range(0, 0));
            birds[i] = Instantiate(birdPrefab, this.transform.position + birdPosition, Quaternion.identity) as GameObject;
//            birds[i].GetComponent<birdBoids>().birdManager = this.gameObject;

        }
    }


    void Update()
    {
        calculateAverageBirdLocation();
    }

    void calculateAverageBirdLocation()
    {

        float birdsX = 0;
        float birdsY = 0;
        int counter = 0;

        foreach (GameObject bird in birds)
        {
            birdsX += bird.transform.position.x;
            birdsY += bird.transform.position.y;
            counter++;
        }

        float averageBirdsX = birdsX / counter;
        //if (averageBirdsX < 0) {
        //	averageBirdsX = Mathf.Abs(averageBirdsX);
        //}
        float averageBirdsY = birdsY / counter;
        //if (averageBirdsY < 0) {
        //	averageBirdsY = Mathf.Abs(averageBirdsY);
        //}
        averageBirdLocation = new Vector3(averageBirdsX, averageBirdsY, 0);

    }


    public float getBirdSpeed()
    {
        return birdSpeed;
    }
}