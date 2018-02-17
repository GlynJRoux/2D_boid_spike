using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animalInitialisation : MonoBehaviour
{

    public GameObject[] birds;
    public GameObject birdPrefab;
    public int birdCounter = 10;
    public Vector3 spawnRange = new Vector3(5, 5, 5);
    public Vector3 averageBirdLocation;

    [Range(0, 150)]
    public int neighbourDistance = 25;

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
            birds[i].GetComponent<boid>().boidManager = this.gameObject;

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
        updateGoalPosition(averageBirdsX, averageBirdsY);
    }

    void updateGoalPosition(float birdsX, float birdsY)
    {

        float goalXLocation = this.transform.position.x;
        float goalYLocation = this.transform.position.y;

        float xDifferential = goalXLocation - birdsX;
        float yDifferential = goalYLocation - birdsY;

        Debug.Log("The X Differential Is " + xDifferential);
        Debug.Log("The Y Differential Is " + yDifferential);

        if ((xDifferential <= 3 && xDifferential >= -3) && (yDifferential <= 3 || yDifferential >= -3))
        {
            //TODO stop it from going out of 0,0 20,20 range

            Vector3 randomGoalLocation = new Vector3((this.transform.position.x + Random.Range(-15.0f, 15.0f)), (this.transform.position.y + Random.Range(-15.0f, 15.0f)), 0);

            if (randomGoalLocation.x > 17.0f)
            {
                randomGoalLocation.Set(17, randomGoalLocation.y, 0);
            }
            else if (randomGoalLocation.x < -17.0f)
            {
                randomGoalLocation.Set(-17, randomGoalLocation.y, 0);
            }

            if (randomGoalLocation.y > 17.0f)
            {
                randomGoalLocation.Set(randomGoalLocation.x, 17, 0);
            }
            else if (randomGoalLocation.y < -17.0f)
            {
                randomGoalLocation.Set(randomGoalLocation.x, -17, 0);
            }

            transform.position = new Vector3((this.transform.position.x + Random.Range(-5.0f, 5.0f)), (this.transform.position.y + Random.Range(-5.0f, 5.0f)), 0);
        }
    }
}