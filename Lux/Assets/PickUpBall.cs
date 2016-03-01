using UnityEngine;
using System.Collections;

public class PickUpBall : MonoBehaviour {

	public float throwForce;

	[Header("Ball Object")]
	public GameObject ball;
	private Transform ballTransform;

	private float distanceFromBall;
	private Transform holdTransform;
	private Transform throwTransform;
	private Vector3 throwPosition;
	private bool holdingBall;

	// Use this for initialization
	void Start () {
		if(ball == null){
			ball = GameObject.FindGameObjectWithTag("Ball");
		}
		ballTransform = ball.transform;
		holdTransform = transform.Find("Hold Position").transform;
		throwTransform = transform.Find("Throw Position").transform;
		holdingBall = false;

		Physics.IgnoreLayerCollision(gameObject.layer, ball.layer);
	}
	
	// Update is called once per frame
	void Update () {
		CalcDistance();
		CheckInput();
	}

	private void CheckInput(){ //Checks distance and Player input and holds ball at holdPosition (1.5f in front of player)
		if(distanceFromBall < 2.5f && Input.GetButton("Pick Up")){
			PickUp();
		} else if (Input.GetButtonUp("Pick Up") && holdingBall && distanceFromBall < 2.5f){
			ThrowBall();
		}
	}

	private void ThrowBall(){
		throwPosition = throwTransform.position;
		Vector3 dir = throwPosition - ballTransform.position;
		float height = dir.y;
		dir.y = 0;
		float dist = dir.magnitude;
		dir.y = dist;
		dist += height;
		float velocity = Mathf.Sqrt(dist * Physics.gravity.magnitude);

		ball.GetComponent<Rigidbody>().velocity = (velocity * dir.normalized) * throwForce;
		
	}

	private void PickUp(){
		ballTransform.position = holdTransform.position;
		ball.transform.rotation = new Quaternion(0, 0, 0, 0);
		holdingBall = true;
	}

	private void CalcDistance(){ //Calculate distance between player and ball
		distanceFromBall = Vector3.Distance(this.transform.position, ballTransform.position);
	}
}
