using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

	public float speed = 30;

	private Vector3 velocity;
	private GameObject laser;

	// Use this for initialization
	void Start () {
	
		velocity = new Vector3 (1, 0, 0) * speed;
		Destroy (gameObject, 10);
		laser = this.gameObject;

	}
	
	// Update is called once per frame
	void Update () {

		if (MissionDemolition.S.mode == GameMode.playing) {

			transform.position = transform.position + velocity * Time.deltaTime;

		}
	
	}

	void OnTriggerEnter(Collider other){

		if(other.tag.Equals("Structure")){

			Destroy(other);
			Destroy(laser);

			velocity = Vector3.zero;

		}
			
	}
		
}
