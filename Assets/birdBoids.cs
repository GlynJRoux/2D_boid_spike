using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdBoids : MonoBehaviour {

    public GameObject birdManager; //World instantiation script
    public Vector2 location; //Current Location
    public Vector2 velocity; //Current Velocity
    public GameObject[] twoClosestBirds;
    [Range(0, 150)]
    public float rotationSpeed = 3; //TODO - Does this do anything anymore? 

    public float angleComparedToWorld = 0;

    private float speed = 0;

    private Vector2 goalPosition;


    // Initialise bird with random velocity and set location variable to the current location of this GameObject
    void Start () {
        location = new Vector2(this.transform.position.x, this.gameObject.transform.position.y);
        velocity = new Vector2(Random.Range(0.1f, 0.01f), Random.Range(0.1f, 0.01f));

        //Invoke repeating allows for this function to be called every so many seconds
        InvokeRepeating("moveTowadsGoal", 5.0f, 8.0f); //Starts after 5 seconds, repeats every 6 seconds
        InvokeRepeating("faceDirectionOfMovement", 5.0f, 8.0f);
    }
	
	// Update is called once per frame
	void Update () {
        goalPosition = getGoalPosition(); //Updates the goal position based on a world object which moves
        setLocation(); //Set variable which is the current location of the object
        setSpeed(); //Updates the birds speed from the world instantiation script
        setAngle(); //Updates the angle compared to (x:location.x, y:10000)
        moveTowardsGoal(); //Function that returns nothing but moves the bird towards it's goal
        //getClosestBirds(); //Returns Array of closest birds within distance of 25
        findTheTwoClosestBirds(birdManager.GetComponent<animalInitialisation>().birds); //Function to avoid collision with local birds
    }

    /*
     * Function to get the current goal position and set the global variable to this 
    */
    Vector2 getGoalPosition(){
        return birdManager.transform.position;
    }

    /*
     * Function to return the current location of this object as a public variable 
    */
    void setLocation(){
        location = this.transform.position;
    }

    /*
     * Function to get the speed of the bird from the world instantiation script and set it for this bird
    */
    void setSpeed(){
        speed = birdManager.GetComponent<animalInitialisation>().birdSpeed;
        //TODO - add a VERY SLIGHT bias to this so that all of the birds are a bit different
        //TODO - Update this intermitently because otherwise the speed would be changing forever (and have that intermitent number random (between 6 and 8 seconds)
        //TODO - This will lead to a slight lag when updating world speed but will make bird behaviour better (look into better ways to code this in future)
    }

    /*
     * Function to find the angle of the bird in degrees. 
     * 0 Will be facing upwards, 90 will be facing right. 
     * //TODO - Probably incorrect (for debugging only so maybe it doesn't matter)
     * //TODO - Probably need to get rid of location.x and add 100000f instead maybe?
    */
    void setAngle(){
        Vector2 worldAngle = new Vector2(location.x, 100000f);
        angleComparedToWorld = Vector2.Angle(worldAngle, location);
    }

    /*
     * Function that returns an array of the closest birds within a set distance to the current object
     * TODO - Make it so that the distance variable is passed through to here (so that it can change for debugging)
    */
    //GameObject[] getClosestBirds(){

        //GameObject[] localBirds = new GameObject[];
      //  int counter = 0;

        //foreach (GameObject isBirdNear in birdManager.GetComponent<animalInitialisation>().birds) {
          //  float distance = Vector2.Distance(location, isBirdNear.transform.position);
        //    if (isBirdNear == this.gameObject) {
          //      continue;
        //    } else if (distance < 10) { //TODO 25 is hardcoded - fix this
      //          localBirds[counter] = isBirdNear; //Put bird which is near into array 
  //              counter++; //Increment the counter
    //        }
//        }

     //   return localBirds;
   // }

    /*
     * A function that will that will find the two closest birds and returns these in an array
    */
    void findTheTwoClosestBirds(GameObject[] localBirds){

        //TODO seperate into 2 functions, one of them is find closest 2 birds. The other is make sure you do not collide with them

        //Variables to find the 2 closest birds
        GameObject closestBird1 = null;
        float closestBird1Distance = 0f;

        GameObject closestBird2 =  null;
        float closestBird2Distance = 0f;

        foreach (GameObject closeBird in localBirds){

            float distanceBetweenObjects = Vector2.Distance(location, closeBird.GetComponent<birdBoids>().location);

            //If closestBird1/2 are unpopulated,populate them.
            if (closestBird1 == null){
                closestBird1 = closeBird;
                closestBird1Distance = distanceBetweenObjects;
            }

            else if (closestBird2 == null){
                closestBird2 = closeBird;
                closestBird2Distance = distanceBetweenObjects;
            }

            //If closest Birds are populated but we find any that are closer, change the closest birds
            else if (closestBird1 != null && closestBird2 != null){
                if(distanceBetweenObjects < closestBird1Distance){
                    closestBird1 = closeBird;
                    closestBird1Distance = distanceBetweenObjects;
                } else if (distanceBetweenObjects < closestBird2Distance){
                    closestBird2 = closeBird;
                    closestBird2Distance = distanceBetweenObjects;
                }
            }
        }
        twoClosestBirds[0] = closestBird1;
        twoClosestBirds[1] = closestBird2;
        avoidTheTwoClosestBirds(twoClosestBirds);
    }

    void avoidTheTwoClosestBirds(GameObject[] twoClosestBirds){
        //TODO check distance and angle between yourself and two closest birds and stop from colliding

        Debug.Log(twoClosestBirds.Length);
        foreach (GameObject closeBird in twoClosestBirds){
            float distanceBetweenClosestBirds = Vector2.Distance(location, closeBird.GetComponent<birdBoids>().location);
            if(distanceBetweenClosestBirds < 5){
                //TODO - Find if bird is on left or right
                //TODO - If on right, turn left slightly,
                //TODO - if on left, turn right slightly
                //TODO - Update Move/rotate towards goal every so many seconds (not all the time)
            }
        }
    }

    /*
     * Function that takes the goal position and moves the bird towards this location
    */
    void moveTowardsGoal() {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, goalPosition, step);

        faceDirectionOfMovement();
    }

    /*
     * Function that will make the bird face the direction which it is travelling in 
    */
    void faceDirectionOfMovement(){

        var direction = goalPosition - location;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //TODO - Slightly change this  based on the birds around you and get rid of goal position to make movements fully random
        //TODO - OR.... change this slightly compared to goal position by -2 to 2 degrees (or something like this) and keep the goal position
        //TODO - If I wish to remove physical goal position, new goal position will be a random number generated by each individual bird added together and divided to find the average

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