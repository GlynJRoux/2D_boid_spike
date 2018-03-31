using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdBoids : MonoBehaviour
{

    public GameObject birdManager; //World instantiation script
    public Vector2 location; //Current Location
    public Vector2 velocity; //Current Velocity
    public GameObject[] twoClosestBirds;
    [Range(0, 150)]
    public float rotationSpeed = 3; //TODO - Does this do anything anymore? 
    public Vector2 randomRangex = new Vector3(95, 95);
    public Vector2 randomRangey = new Vector3(95, 95);
    private Vector2 RandomTarget;
    public float randomiseDirectionTime = 4f;
    public float angleComparedToWorld = 0;
    double nextUsage;
    double delay;
    double randomDouble;
    private float speed = 0;
    public float step;
    Vector2 foodLocation;
    int foodSight = 5;
    int energy = 100;
    int energyBoost = 10;


    // Initialise bird with random velocity and set location variable to the current location of this GameObject
    void Start()
    {
        randomDouble = UnityEngine.Random.value;

        location = new Vector2(this.transform.position.x, this.gameObject.transform.position.y);
        velocity = new Vector2(UnityEngine.Random.Range(1f, 3f), UnityEngine.Random.Range(1f, 3f));
        RandomTarget = new Vector3(UnityEngine.Random.Range(-randomRangex.x, randomRangex.x),
                                     UnityEngine.Random.Range(-randomRangey.y, randomRangey.y));
        delay = UnityEngine.Random.Range(5.0f, 15.0f);
        nextUsage = Time.time + delay + randomDouble;

    }

    // Update is called once per frame
    void Update()
    {
//        Debug.Log(foodLocation);
        setLocation(); //Set variable which is the current location of the object
        checkIfEating();
        RandomTarget = getRandomTargetPosition();
        setSpeed(); //Updates the birds speed from the world instantiation script
        setAngle(); //Updates the angle compared to (x:location.x, y:10000)
        moveToRandomPosition();
        findTheTwoClosestBirds(animalInitialisation.birds); //Function to avoid collision with local birds


    }


    Vector3 getRandomTargetPosition()
    {
        Vector2 randomMove;
        if (Time.time > nextUsage)
        {
            randomMove = new Vector3(UnityEngine.Random.Range(-randomRangex.x, randomRangex.x),
                                    UnityEngine.Random.Range(-randomRangey.y, randomRangey.y));
            nextUsage = Time.time + delay;
        }

        else
        {

            randomMove = RandomTarget;
        }
        int nearestFood = foodSight + 1;
        int nearestIndex = -1;

        for (int i = 0; i < FoodInitialisation.foodCounter; i++)
        {
            if (FoodInitialisation.foods[i] != null)
            {
                Vector3 foodLocation = FoodInitialisation.foods[i].transform.position;
                int currentNearest = Math.Min(Math.Abs((int)(this.location.y - foodLocation.y)), Math.Abs((int)(this.location.x - foodLocation.x)));
                if (currentNearest < nearestFood)
                {
                    nearestFood = currentNearest;
                    nearestIndex = i;
                }
            }
        }
        if (nearestIndex >= 0)
        {
            randomMove = FoodInitialisation.foods[nearestIndex].transform.position;
        }



        return randomMove;

    }


    private void moveToRandomPosition()
    {
        if (energy > 0)
        {
            Debug.Log("Energy: " + energy);
            step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, RandomTarget, step);
            faceDirectionOfMovement();
            energy--;
        } else
        {
            Debug.Log("Bird Died");
        }
    }

    void faceDirectionOfMovement()
    {


        var distance = RandomTarget - location;
        var angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


    }
    /*
     * Function to return the current location of this object as a public variable 
    */
    void setLocation()
    {
        location = this.transform.position;
    }

    /*
     * Function to check if  bird is at food
    */
    void checkIfEating()
    {
        float tolerance = 3.0f;

        for (int i = 0; i < FoodInitialisation.foodCounter; i++)
        {
            if (FoodInitialisation.foods[i] != null)
            {
                //   Debug.Log("Checking food(" + i + ")");
                Vector3 foodLocation = FoodInitialisation.foods[i].transform.position;
                if (Vector3.Distance(location, foodLocation) < tolerance)
                {
                    //    Debug.Log("Bird eats food!");
                    energy += energyBoost;
                    Destroy(FoodInitialisation.foods[i]);

                }
            }
        }
    }

    /*
     * Function to get the speed of the bird from the world instantiation script and set it for this bird
    */
    void setSpeed()
    {
        float RandomVariation = UnityEngine.Random.value;
        speed = animalInitialisation.birdSpeed + RandomVariation;

    }

    /*
     * Function to find the angle of the bird in degrees. 
    */
    void setAngle()
    {
        Vector2 worldAngle = new Vector2(location.x, 100000f);
        angleComparedToWorld = Vector2.Angle(worldAngle, location);
    }

    /*
     * A function that will that will find the two closest birds 
    */
    void findTheTwoClosestBirds(GameObject[] localBirds)
    {

        twoClosestBirds = new GameObject[2];

        //Variables to find the 2 closest birds
        GameObject closestBird1 = null;
        float closestBird1Distance = 0f;


        GameObject closestBird2 = null;
        float closestBird2Distance = 0f;

        foreach (GameObject closeBird in localBirds)
        {

            float distanceBetweenObjects = Vector2.Distance(location, closeBird.GetComponent<birdBoids>().location);

            //If closestBird1/2 are unpopulated,populate them.
            if (closestBird1 == null && closeBird != this.gameObject)
            {
                closestBird1 = closeBird;
                closestBird1Distance = distanceBetweenObjects;
            }

            else if (closestBird2 == null && closeBird != this.gameObject)
            {
                closestBird2 = closeBird;
                closestBird2Distance = distanceBetweenObjects;
            }

            //If closest Birds are populated but we find any that are closer, change the closest birds
            else if (closestBird1 != null && closestBird2 != null && closeBird != this.gameObject)
            {
                if (distanceBetweenObjects < closestBird1Distance)
                {
                    closestBird1 = closeBird;
                    closestBird1Distance = distanceBetweenObjects;
                }
                else if (distanceBetweenObjects < closestBird2Distance)
                {
                    closestBird2 = closeBird;
                    closestBird2Distance = distanceBetweenObjects;
                }
            }
        }
        twoClosestBirds[0] = closestBird1;
        twoClosestBirds[1] = closestBird2;
        avoidTheTwoClosestBirds(twoClosestBirds);
    }

    void avoidTheTwoClosestBirds(GameObject[] twoClosestBirds)
    {

        foreach (GameObject closeBird in twoClosestBirds)
        {


            float distanceBetweenClosestBirds = Vector2.Distance(location, closeBird.GetComponent<birdBoids>().location);

            if (distanceBetweenClosestBirds <= 0.70)
            {
                RandomTarget = new Vector3(UnityEngine.Random.Range(-randomRangex.x, randomRangex.x),
                                     UnityEngine.Random.Range(-randomRangey.y, randomRangey.y));
                moveToRandomPosition();


            }

            if (distanceBetweenClosestBirds < 6)
            {
                GameObject CloseBird1 = twoClosestBirds[0];
                GameObject CloseBird2 = twoClosestBirds[1];

                Vector3 avarageAngle = new Vector3(((CloseBird1.transform.position.x + CloseBird2.transform.position.x + this.transform.position.x) / 3), ((CloseBird1.transform.position.y + CloseBird2.transform.position.y + this.transform.position.y) / 3), 0);

                CloseBird1.transform.Rotate(avarageAngle);
                CloseBird2.transform.Rotate(avarageAngle);
                this.transform.Rotate(avarageAngle);



            }

        }
    }

}









//TODO - Read up on this - https://docs.unity3d.com/ScriptReference/Time-deltaTime.html 





//TODO Need to make a function to return average of the local flock (25 distance from current boid)
//TODO Call this function from near the top  of Update()

//separation: steer to avoid crowding local flockmates
//cohesion: steer to move toward the average position(center of mass) of local flockmates

//----------------------------------------------------------------------------------

//&&&&&&&&&&&&&//alignment: steer towards the average heading of local flockmates
//Alignment will be biased and will be affected by goalPosition
//To do this we want to set goal position as an offset of alignment
//Take Find angle of rotation of this object (lookg straight at goal pos)
//+/- a few degrees to give a change compared to the goal pos (Update this less than everything else)
//Get angle of all local birds within set distance (25)
//Add this to the bias
//This should be your current heading (allows for individual flocking towards a goal with groups average as bias

//----------------------------------------------------------------------------------