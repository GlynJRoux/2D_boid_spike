﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdBoids : MonoBehaviour {

    public GameObject birdManager; //World instantiation script
    public Vector2 location; //Current Location
    public Vector2 velocity; //Current Velocity

    [Range(0,150)]
    public float speed = 3;



    // Initialise bird with random velocity and set location variable to the current location of this GameObject
    void Start () {
        location = new Vector2(this.transform.position.x, this.gameObject.transform.position.y);
        velocity = new Vector2(Random.Range(0.1f, 0.01f), Random.Range(0.1f, 0.01f));
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 goalPosition = getGoalPosition(); //Updates the goal position based on a world object which moves
        moveTowardsGoal(goalPosition); //Function that returns nothing but moves the bird towards it's goal
        GameObject[] localBirds = getClosestBirds(); //Returns Array of closest birds within distance of 25
        avoidLocalBirds(localBirds); //Function to avoid collision with local birds
    }

    /*
     * Function to get the current goal position and set the global variable to this 
    */
    Vector2 getGoalPosition(){
        return birdManager.transform.position;
    }

    /*
     * Function that returns an array of the closest birds within a set distance to the current object
     * TODO - Make it so that the distance variable is passed through to here (so that it can change for debugging)
    */
    GameObject[] getClosestBirds(){

        GameObject[] localBirds = new GameObject[40];
        int counter = 0;

        foreach (GameObject isBirdNear in birdManager.GetComponent<animalInitialisation>().birds) {
            if (isBirdNear == this.gameObject) {
                continue;
            } else if (Vector2.Distance(location, isBirdNear.GetComponent<birdBoids>().location) < 25) { //TODO 25 is hardcoded - fix this
                localBirds[counter] = isBirdNear; //Put bird which is near into array 
                counter++; //Increment the counter
            }
        }

        return localBirds;
    }

    //
    void avoidLocalBirds(GameObject[] localBirds){

        foreach (GameObject closeBird in localBirds){
            if (Vector2.Distance(location, closeBird.GetComponent<birdBoids>().location) < 3){ //TODO - Mess around with this variable
                //TODO - Avoid these brids somehow 
                //TODO - Use quanternium slerp to move the direction of travel away from the other bird?
            }
        }
    }

    /*
     * Function that takes the goal position and moves the bird towards this location
    */
    void moveTowardsGoal(Vector2 goalPosition) {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, goalPosition, step);
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