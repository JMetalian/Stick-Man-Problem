//Adaptive Optimization Implementation Project
//The Stick Man Problem
//Written on C# using Unity Engine
//Used Algorithm for optimization: Simulated Annealing
//İrem Ayvaz-Abdulwahab Hajar-Can Kozan-İbrahim Krasniqi


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerJump : MonoBehaviour {
	static System.Random ranat= new System.Random();
    public static Rigidbody2D playerBody;

    public static bool onTheGround=false;

    [SerializeField]double jumpFactor;

    public static double jumpF=10; // INITIAL JUMP FACTOR 
    public static int Score = 0; //Score

    double tempJumpFactor = jumpF;
    static double Tinit = 20, m = 10, n = 3;
    double coolingFactor = 0.90;
    double T = Tinit;
    int previousScore = PlayerJump.Score;
    int currentScore = 0;
    int bestScore = 0;
    double bestJump = 0;
    double temp = 0;
    int counter = 0;
    bool isAccepted = false;
    bool firstRound = true;

    //hopefully this works

    void OnCollisionEnter2D(Collision2D coll)
    {
       
        {

            // Counter determines iterations of inner loop in cannonical form before temperature is changed
            counter++;
            if ((counter % 5) == 0)
            {
                T *= coolingFactor;
                counter = 0;

            }



            if (coll.gameObject.tag.Equals("box_tag") || coll.gameObject.tag.Equals("ceiling_tag"))
            {
                if (firstRound)
                {
                    firstRound = false;
                    currentScore = PlayerJump.Score - previousScore;
                    bestJump = jumpF;
                    bestScore = currentScore;

                    // for next round
                    previousScore = currentScore;
                    temp = jumpF; //store original jump value, in case next value isn't accepted
                    tempJumpFactor = move(temp, ranat);
                    jumpF = tempJumpFactor;
                    PlayerJump.Score = 0; // Reset the score
                    Application.LoadLevel("SampleScene");
                }
                else
                {
                    currentScore = PlayerJump.Score; //assuming score was reset, otherwise use the one below it 
                                                     //currentScore = PlayerJump.Score - previousScore;
                    isAccepted = SimulatedAnnealing(currentScore, previousScore, T);
                    if (isAccepted == true)
                    {
                        // if score is better, update best solution
                        if (currentScore > bestScore)
                        {
                            bestScore = currentScore;
                            bestJump = jumpF;
                        }

                        temp = jumpF; //store original jump value, in case next value isn't accepted
                        tempJumpFactor = move(temp, ranat);
                        jumpF = tempJumpFactor;
                        PlayerJump.Score = 0; // Reset the score
                        Application.LoadLevel("SampleScene");
                    }

                    // if solution isn't accepted, reject move, restore original values and start searching again
                    else
                    {
                        jumpF = temp;
                        tempJumpFactor = move(temp, ranat);
                        jumpF = tempJumpFactor;
                        PlayerJump.Score = 0; //reset score

                        Application.LoadLevel("SampleScene");
                    }


                }
            }


        }
    }

    //Method which determines whether to accept the solution or not
    //returns true for accepted, false for unaccepted
    public static Boolean SimulatedAnnealing(int score, int previousScore, double T)
    {
        if (score > previousScore)
        {
            return true;
        }

        else return Metropolis(score, previousScore, T, PlayerJump.ranat);
    }

    //Method which simulates the metropolis criterion
    public static Boolean Metropolis(double score, double previousScore, double T, System.Random rand)
    {
        if (rand.NextDouble() <= Math.Pow(-(Math.E), ((score - previousScore) / T)))
        {
            return true;
        }
        else return false;
    }


    void Start () {
       // jumpF = jumpFactor;
		playerBody=GetComponent<Rigidbody2D>();
	}
	void Update(){
		//if(onTheGround==false){
			//playerBody.velocity=new Vector2(0,jumpFactor);
			//onTheGround=true;
		//}
		if(playerBody.velocity.y==0){
			onTheGround=false;
		}
	}
    public static void jump(double jFactor)
    {
       playerBody.velocity = new Vector2(0, (float)jFactor);
    }

	public static double move(double number, System.Random ranat){
        double randStdNormal;
        do
        {
            double u1 = 1.0 - ranat.NextDouble(); //uniform(0,1] random doubles
            double u2 = 1.0 - ranat.NextDouble();
            randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
            Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
        } while ((number + randStdNormal) < 0);
			return number+randStdNormal;

	}
 
    
	}
