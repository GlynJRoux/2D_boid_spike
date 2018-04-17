
using UnityEngine;

public class BirdBoids : MonoBehaviour
{

    public GameObject ballManager, birdManager, foodManager;
    public Vector2 location; //Current Location
    public Vector2 velocity; //Current Velocity
	float neighbourDistance;
    public GameObject[] twoClosestBirds;
    [Range(0, 150)]
    public float rotationSpeed = 3; //TODO - Does this do anything anymore? 
	int spawnRange;
    private Vector2 RandomTarget;
    public float randomiseDirectionTime = 4f;
    public float angleComparedToWorld = 0;
    double nextUsage;
    double delay;
    double randomDouble;
    private float speed = 0;
    public float step;
    Vector2 foodLocation;
    int energy;
    int energyBoost;
	int foodCounter;
	GameObject[] birds,foods;
	float foodSight;
	bool inFlock, foundFood;


    // Initialise bird with random velocity and set location variable to the current location of this GameObject
    void Start()
    {
		birds = birdManager.GetComponent<AnimalInitialisation>().birds;
		foods = foodManager.GetComponent<FoodInitialisation>().foods;
		spawnRange = ballManager.GetComponent<BallInitialisation>().spawnRange;
        randomDouble = UnityEngine.Random.value;
		foodCounter = foodManager.GetComponent<FoodInitialisation>().foodCounter;
		foodSight = birdManager.GetComponent<AnimalInitialisation>().foodSight;
		neighbourDistance = birdManager.GetComponent<AnimalInitialisation>().neighbourDistance;
		energy = birdManager.GetComponent<AnimalInitialisation>().energy;
		energyBoost = birdManager.GetComponent<AnimalInitialisation>().energyBoost;

        location = new Vector2(this.transform.position.x, this.gameObject.transform.position.y);
        velocity = new Vector2(UnityEngine.Random.Range(1f, 3f), UnityEngine.Random.Range(1f, 3f));
        RandomTarget = new Vector2(UnityEngine.Random.Range(-spawnRange, +spawnRange),
                                     UnityEngine.Random.Range(-spawnRange, +spawnRange));
        delay = UnityEngine.Random.Range(5.0f, 15.0f);
        nextUsage = Time.time + delay + randomDouble;

    }

    // Update is called once per frame
    void Update()
    {
        if (energy == 0) Destroy(this.gameObject);

        setLocation(); //Set variable which is the current location of the object
		setSpeed(); //Updates the birds speed from the world instantiation script
        checkIfEating();
        RandomTarget = getRandomTargetPosition();
		inFlock = false;
		Vector2 flockTarget = checkFlock();
//		Debug.Log("flock centre=" + flockTarget);
		foundFood=false;
		Vector2 foodTarget = findFood();
		if (inFlock&&!foundFood)
			RandomTarget = (RandomTarget+flockTarget)/2;
		else if (inFlock&&foundFood)
			RandomTarget = (RandomTarget+flockTarget+foodTarget)/3;
        moveToRandomPosition();
//        findTheTwoClosestBirds(birds); //Function to avoid collision with local birds
    }


    Vector2 getRandomTargetPosition()
    {
        Vector2 randomMove;
        if (Time.time > nextUsage)
        {
			randomMove = new Vector3(UnityEngine.Random.Range(-spawnRange, +spawnRange),
				UnityEngine.Random.Range(-spawnRange, +spawnRange),0);
            nextUsage = Time.time + delay;
//			Debug.Log("random=" + randomMove);
        }

        else
        {

            randomMove = RandomTarget;
        }

        return randomMove;

    }

	Vector2 findFood() {
		Vector2 nearestFoodLocation = Vector2.zero;
		float nearestFood = foodSight + 1;
		int nearestIndex = -1;

		for (int i = 0; i < foodCounter; i++)
		{
			if (foods[i] != null)
			{
				Vector3 foodLocation = foods[i].transform.position;
				float currentNearest = Vector3.Distance(foodLocation,location);
				if ((currentNearest < nearestFood)&&(this.gameObject != foods[i].GetComponent<BirdFood>().lastBird))
				{
					nearestFood = currentNearest;
					nearestIndex = i;
				}
			}
		}
		if (nearestIndex >= 0)
		{
			nearestFoodLocation = foods[nearestIndex].transform.position;
			foundFood = true;
		}
		return nearestFoodLocation;
	}

	Vector3 checkFlock() {
		Vector2 flockCenter = Vector2.zero;
		int flockSize=0;
		foreach (GameObject bird in birds) {
			if (bird != this.gameObject&&bird!=null) {
				if (Vector2.Distance(location, bird.GetComponent<BirdBoids>().location) < neighbourDistance) {
					flockCenter += bird.GetComponent<BirdBoids>().location;
					flockSize++;
//					Debug.Log("found bird in flock=" + flockSize);
				}
			}
		}
		if (flockSize > 0) {
			flockCenter /= flockSize;
			inFlock = true;
		}
//		Debug.Log("flockcenter=" + flockCenter + " of size " + flockSize);
		return flockCenter;
	}
					


    private void moveToRandomPosition()
    {
        if (energy > 0)
        {
 //           Debug.Log("Energy: " + energy);
            step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(location, RandomTarget, step);
            faceDirectionOfMovement();
            energy--;
        } else
        {
 //           Debug.Log("Bird Died");
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

        for (int i = 0; i<foodCounter; i++)
        {
            if (foods[i] != null)
            {
                //   Debug.Log("Checking food(" + i + ")");
                Vector3 foodLocation = foods[i].transform.position;
                //              if ((Vector3.Distance(location, foodLocation) < tolerance)&&(this.gameObject!= FoodInitialisation.foods[i].GetComponent<BirdFood>().lastBird))
                if ((Vector3.Distance(location, foodLocation) < tolerance))
                {
   //                 Debug.Log("Bird eats food!");
                    energyBoost = foods[i].GetComponent<BirdFood>().energy;
                    int eatenEnergy = UnityEngine.Random.Range(1, energyBoost);
                    energy += eatenEnergy;
                    foods[i].GetComponent<BirdFood>().lastBird = this.gameObject;
                    foods[i].GetComponent<BirdFood>().energy -= eatenEnergy;
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
        speed = birdManager.GetComponent<AnimalInitialisation>().birdSpeed + RandomVariation;

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

		foreach (GameObject closeBird in localBirds) {
//			Debug.Log(closeBird);
			if (closeBird != null) {
				float distanceBetweenObjects = Vector2.Distance(location, closeBird.GetComponent<BirdBoids>().location);

				//If closestBird1/2 are unpopulated,populate them.
				if (closestBird1 == null && closeBird != this.gameObject) {
					closestBird1 = closeBird;
					closestBird1Distance = distanceBetweenObjects;
				} else if (closestBird2 == null && closeBird != this.gameObject) {
					closestBird2 = closeBird;
					closestBird2Distance = distanceBetweenObjects;
				}

            	//If closest Birds are populated but we find any that are closer, change the closest birds
            	else if (closestBird1 != null && closestBird2 != null && closeBird != this.gameObject) {
					if (distanceBetweenObjects < closestBird1Distance) {
						closestBird1 = closeBird;
						closestBird1Distance = distanceBetweenObjects;
					} else if (distanceBetweenObjects < closestBird2Distance) {
						closestBird2 = closeBird;
						closestBird2Distance = distanceBetweenObjects;
					}
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


            float distanceBetweenClosestBirds = Vector2.Distance(location, closeBird.GetComponent<BirdBoids>().location);

            if (distanceBetweenClosestBirds <= 0.70)
            {
				RandomTarget = new Vector3(UnityEngine.Random.Range(-spawnRange, +spawnRange),
					UnityEngine.Random.Range(-spawnRange, +spawnRange),0);
                moveToRandomPosition();


            }

            if (distanceBetweenClosestBirds < 6)
            {
                GameObject CloseBird1 = twoClosestBirds[0];
                GameObject CloseBird2 = twoClosestBirds[1];

                Vector3 avarageAngle = new Vector3(((CloseBird1.transform.position.x + CloseBird2.transform.position.x + this.transform.position.x) / 3), 
                    ((CloseBird1.transform.position.y + CloseBird2.transform.position.y + this.transform.position.y) / 3), 0);

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