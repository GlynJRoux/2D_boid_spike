using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdFood : MonoBehaviour {
    public GameObject foodManager; //World instantiation script
    public Vector2 location; //Current Location
    public Vector3 seenRange = new Vector3(5, 5, 5); // current range that food can be seen from 

    // Use this for initialization
    void Start () {
        location = new Vector2(this.transform.position.x, this.gameObject.transform.position.y);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

}
