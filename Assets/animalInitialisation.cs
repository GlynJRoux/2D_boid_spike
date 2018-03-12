using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animalInitialisation : MonoBehaviour
{

    public GameObject[] birds;
    public GameObject birdPrefab;
    public int birdCounter = 10;
    public Vector3 spawnRange = new Vector3(35, 35,10);
    public Vector3 averageBirdLocation;

    [Range(0, 150)]
    public float birdSpeed = 3;

    [Range(0, 150)]
    public double neighbourDistance = 1;

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
            Vector3 birdPosition = new Vector3(Random.Range(-spawnRange.x, spawnRange.x),
                                  Random.Range(-spawnRange.y, spawnRange.y),
                                  Random.Range(0, 0));
            birds[i] = Instantiate(birdPrefab, this.transform.position + birdPosition, Quaternion.identity) as GameObject;
            birds[i].GetComponent<birdBoids>().birdManager = this.gameObject;

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
        int counter = 1;

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