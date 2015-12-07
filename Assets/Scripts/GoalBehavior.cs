using UnityEngine;
using System.Collections;

public class GoalBehavior : MonoBehaviour {

	static public bool goalMet;

	void Awake(){

		goalMet = false;

	}

	void OnTriggerEnter(Collider other){

		if (other.gameObject.tag.Equals ("Shot")) {

			GoalBehavior.goalMet = true;

			Color c = renderer.material.color;
			c.r = 1;
			c.b = 0;
			c.g = 0;
			renderer.material.color = c;

		}

	}

}
