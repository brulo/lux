using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform targetTransform;
	[Header("Offsets")]
	public float yOffset;
	public float zOffset;

	[Header("X Rotation")]
	[Range(0.0f, 90.0f)]
	public float xRotation;



	// Use this for initialization
	void Start () {
		setRotation();
	}
	
	// Update is called once per frame
	void Update () {
		setPosition();
		setRotation(); 
		//print(Vector3.Distance(targetTransform.position, transform.position));
	}

	private void setPosition(){
		Vector3 newPosition = new Vector3(targetTransform.position.x, targetTransform.position.y + yOffset, targetTransform.position.z + zOffset);
		transform.position = newPosition;
	}

	private void setRotation(){
		if(transform.rotation.x != xRotation){
			transform.eulerAngles = new Vector3(xRotation, 0, 0);
		}
	}
}
