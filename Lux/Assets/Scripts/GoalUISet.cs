using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GoalUISet : MonoBehaviour {

	void Start(){
		SetUI(0);
	}

	public void SetUI(int goal){
		this.gameObject.GetComponent<Text>().text = "Goals: " + goal;
	}
}
