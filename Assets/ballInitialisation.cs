using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballInitialisation : MonoBehaviour {
    public GameObject circle;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)){
            Vector3 spawnPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            spawnPoint.z = 0.0f;

            GameObject objectInstance = Instantiate(circle, spawnPoint, Quaternion.Euler(new Vector3(0, 0, 0)));

        }
		
	}
}
