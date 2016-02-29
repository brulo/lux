using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	public float turnSmoothing = 6f;
	public float speedDampTime = 0.1f;
	public float speed = 10f;
	public float jumpForce = 5f;
	public bool grounded;
	public float gravity = -25;

	private Animator anim;
	private Rigidbody rigbod;
	private float h;
	private float v;
	private bool jump;
	private bool jumpWait = true;

	void Start(){
		//anim = GetComponent<Animator>();
		rigbod = GetComponent<Rigidbody>();
		Physics.gravity = new Vector3(0f, gravity, 0f);
	}

	void FixedUpdate ()
	{
		jump = Input.GetButton("Jump");
		h = Input.GetAxis("Horizontal");
		v = Input.GetAxis("Vertical");
		SetSpeed();
		MovementManagement(h, v);
		Jump();
	}

	void OnCollisionEnter(Collision col){
		if(col.gameObject.tag == "Ground"){
			//Ground detection, though I'm sure this could be achieved through
			//Raycasts or whatever
			grounded = true;
		}
	}

	void Jump(){
		//General jump function
		if(jump && grounded && jumpWait){
			StartCoroutine(JumpWaitCo());
			jumpWait = false;
			jump = false;
			grounded = false;
			rigbod.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
		}
	}

	IEnumerator JumpWaitCo(){
		//Purpose of this function is to prevent a sort of "double jump" when jumping onto a platform
		//and hitting the edge.. Would cause the player to jump ~2x farther than normal
		yield return new WaitForSeconds(.25f);
		jumpWait = true;
	}

	void SetSpeed(){
		//Set speed so that you don't move faster in the diagonal directions
		if(Mathf.Abs(h) > 0 && Mathf.Abs(v) > 0){
			h = h * (speed * .75f) * Time.deltaTime;
			v = v * (speed * .75f) * Time.deltaTime;
		} else {
			h = h * speed * Time.deltaTime;
			v = v * speed * Time.deltaTime;
		}
	}

	void MovementManagement(float horizontal, float vertical){
		Vector3 newMovePosition = new Vector3(transform.position.x + horizontal, transform.position.y, transform.position.z + vertical);
		if(horizontal != 0f || vertical != 0f){
			Rotating(horizontal, vertical);
			rigbod.MovePosition(newMovePosition);
			//anim.SetFloat(speed, xxx, speedDampTime, Time.deltaTime);
		} else {
			//anim.SetFloat(speed, xxx);
		}
	}

	void Rotating(float horizontal, float vertical){
		//Rotates the character in the direction of movement
		Vector3 targetDirection = new Vector3(horizontal, 0f, vertical);
		Quaternion targetRoation = Quaternion.LookRotation(targetDirection, Vector3.up);
		Quaternion newRotation = Quaternion.Lerp(rigbod.rotation, targetRoation, turnSmoothing * Time.deltaTime);
		transform.rotation = newRotation;
	}


}