using UnityEngine;
using System.Collections;

public class GoalTracker : MonoBehaviour {

	public GameObject goalText;

	private GameObject ball;
	private Vector3 ballSpawn;
	private int goals;

	public int Goals{
		get {return goals;}
	}


	void Start(){
		ball = GameObject.FindGameObjectWithTag("Ball");
		ballSpawn = ball.transform.position;
	}
		
	void OnCollisionEnter(Collision col){
		if(col.gameObject == ball){
			goals += 1;
			ball.transform.position = ballSpawn;
			goalText.GetComponent<GoalUISet>().SetUI(goals);
			//Reload scene/Reload spawn positions etc
		}
	}
}
