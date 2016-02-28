using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform targetTransform;
	[Header("Offsets")]
	public float yOffset;
	public float zOffset;

	// Use this for initialization
	void Start () {
		setRotation();
	}
	
	// Update is called once per frame
	void Update () {
		setTransform();
		//setRotation(); 
		//print(Vector3.Distance(targetTransform.position, transform.position));
	}

	private void setTransform(){
		Vector3 newPosition = new Vector3(targetTransform.position.x, targetTransform.position.y + yOffset, targetTransform.position.z + zOffset);
		transform.position = newPosition;
	}

	private void setRotation(){
		if(transform.rotation.x != 35f){
			transform.eulerAngles = new Vector3(35f, 0, 0);
		}
	}
}
