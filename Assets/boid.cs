using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO Comments
//TODO Better structured code (Call everything from update IF POSSIBLE)
//TODO Randomness function
//TODO MAKE THEM FACE THE PROPER WAY - Quanternion Slerp

public class boid : MonoBehaviour
{

    public GameObject boidManager; //World Instantiation Script Object
    public Vector2 location; //Current Location
    public Vector2 velocity; //Current Velocity
    public Vector2 goalPos; //Position which we wish to get to TODO Remove this
    Vector2 currPos; //Current position of this object
    Vector2 currForce; //Current force of this object


    //Initialise with random velocity and location
    void Start()
    {
        velocity = new Vector2(Random.Range(0.1f, 0.1f), Random.Range(0.1f, 0.01f));
        location = new Vector2(this.transform.position.x, this.gameObject.transform.position.y);
    }

    //Function that takes a targets location and returns TODO UP TO HERE
    Vector2 findTarget(Vector2 target)
    {
        return (target - location);
    }

    void applyForce(Vector2 force)
    {

        Vector3 f = new Vector3(force.x, force.y, 0);
        if (f.magnitude > boidManager.GetComponent<animalInitialisation>().maxForce)
        {
            f = f.normalized;
            f *= boidManager.GetComponent<animalInitialisation>().maxForce;
        }
        this.GetComponent<Rigidbody2D>().AddForce(f);

        if (this.GetComponent<Rigidbody2D>().velocity.magnitude > boidManager.GetComponent<animalInitialisation>().maxVelocity)
        {
            this.GetComponent<Rigidbody2D>().velocity = this.GetComponent<Rigidbody2D>().velocity.normalized;
            this.GetComponent<Rigidbody2D>().velocity *= boidManager.GetComponent<animalInitialisation>().maxVelocity;
        }

    }



    Vector2 AllignWithGroup()
    {
        double neighbourDist = boidManager.GetComponent<animalInitialisation>().neighbourDistance;
        Vector2 sum = Vector2.zero;
        int count = 0;
        foreach (GameObject other in boidManager.GetComponent<animalInitialisation>().birds)
        {
            if (other == this.gameObject)
                continue;
            double d = Vector2.Distance(location, other.GetComponent<boid>().location);
            if (d < neighbourDist)
            {
                sum = other.GetComponent<boid>().velocity;
                count++;
            }
        }
        if (count > 0)
        {
            sum /= count;
            Vector2 direction = sum - velocity;
            return direction;
        }
        return Vector2.zero;
    }

    Vector2 CohesionOfGroup()
    {
        double neighbourDist = boidManager.GetComponent<animalInitialisation>().neighbourDistance;
        Vector2 sum = Vector2.zero;
        int count = 0;
        foreach (GameObject other in boidManager.GetComponent<animalInitialisation>().birds)
        {
            if (other == this.gameObject)
                continue;
            double d = Vector2.Distance(location, other.GetComponent<boid>().location);
            if (d < neighbourDist)
            {
                sum += other.GetComponent<boid>().location;
                count++;
            }
        }
        if (count > 0)
        {
            sum /= count;
            return findTarget(sum);
        }
        return Vector2.zero;

    }
    void flock()
    {
        location = this.transform.position;
        velocity = this.GetComponent<Rigidbody2D>().velocity;

        if (Random.Range(0, 10) <= 1)
        {
            Vector2 allign = AllignWithGroup();
            Vector2 cohesion = CohesionOfGroup();
            currForce = goalPos + allign + cohesion;
            currForce = currForce.normalized;
        }


        applyForce(currForce);
    }



    // Update is called once per frame
    void Update()
    {
        goalPos = boidManager.transform.position;
        flock();
        Debug.DrawRay(transform.position, boidManager.transform.position, Color.green);
    }
}
