//Adaptive Optimization Implementation Project
//The Stick Man Problem
//Written on C# using Unity Engine
//Used Algorithm for optimization: Simulated Annealing
//İrem Ayvaz-Abdulwahab Hajar-Can Kozan-İbrahim Krasniqi

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawn : MonoBehaviour {
    private float nextSpawn;
    private float randomScaler;
    [SerializeField] private GameObject box;
    [SerializeField] float spawnDelay;
    
    


    void Start()
    {
        nextSpawn = spawnDelay;
    }
    //OPERATION AREA
    void Update () {
        //spawnDelay = Random.Range(0.7f, 1.9f);
        if (ShouldSpawn()){
            nextSpawn = Time.time + 1.5f ;  /// IT CAN BE CHANGED FOR VARIATIONS
            //spawnDelay;

            randomScaler = Random.Range(0, 0.005f);            
            box.transform.localScale -= new Vector3(0,randomScaler,0);  // NEW SCALER ADDED FOR THE BOXES - THANKS TO YUNUS ATAHAN

            Instantiate(box,new Vector3(10,1,0),Quaternion.identity);
            print("New spawned");
        }
		
	}


    bool ShouldSpawn()
    {
        return Time.time >= nextSpawn;
    }
}
