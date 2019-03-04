//Adaptive Optimization Implementation Project
//The Stick Man Problem
//Written on C# using Unity Engine
//Used Algorithm for optimization: Simulated Annealing
//İrem Ayvaz-Abdulwahab Hajar-Can Kozan-İbrahim Krasniqi


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour {

	public Rigidbody2D rigidbodyBox;
    public bool ScoreHappenend = false;
    [SerializeField] GameObject player;
    [SerializeField] public static float jump;
    
    
	public float simulatedAnnealingRes;
	void Start () {
		rigidbodyBox=GetComponent<Rigidbody2D>();
		rigidbodyBox.velocity=new Vector2(-15,0);
	}
    
    private void Update()
    {
        
        if((this.transform.position.x - player.transform.position.x <= 4)&&player.transform.position.y <= 1.8)
        {
            PlayerJump.jump(PlayerJump.jumpF);
        }
        if(this.transform.position.x <= player.transform.position.x && !ScoreHappenend)
        {
            PlayerJump.Score += 1;
            print(PlayerJump.Score);
            ScoreHappenend = true;
        }
        if(this.transform.position.x <= -10)
        {
            Destroy(this.gameObject);
            
        }
    }
   

}
