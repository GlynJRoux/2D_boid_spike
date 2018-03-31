using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdFood : MonoBehaviour
{
    public GameObject foodManager; //World instantiation script
    public int energy;
   

    // Use this for initialization
    void Start()
    {
        energy = UnityEngine.Random.Range(1, 20);
    }

    // Update is called once per frame
    void Update()
    {
        

    }



}



