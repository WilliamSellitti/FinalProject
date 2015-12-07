using UnityEngine;
using System.Collections;

//V 2.0 updated code

public enum ObstaclePos{

	pos1,
	pos2,
	pos3,
	pos4

}

public class Obstacle : MonoBehaviour {

	static public Obstacle S;
	public ObstaclePos posNum;

	public float maxHeight = 25;
	public float speed1 = 1.0f;
	public float speed2 = 1.5f;
	public float speed3 = 2.0f;
	public float speed4 = 2.5f;

	private Vector3 startingPos;
	private Vector3 tempPos;

	void Awake(){

		S = this;
		startingPos = this.transform.position;

	}

	// Use this for initialization
	void Start () {
	
		switch (posNum) {

		case ObstaclePos.pos1:
			startingPos.x = 60;
			break;

		case ObstaclePos.pos2:
			startingPos.x = 50;
			break;

		case ObstaclePos.pos3:
			startingPos.x = 40;
			break;

		case ObstaclePos.pos4:
			startingPos.x = 30;
			break;

		}

	}
	
	// Update is called once per frame
	void Update () {
		if (MissionDemolition.S.mode == GameMode.playing) {

			switch (posNum) {
			
			case ObstaclePos.pos1:
				tempPos.x = startingPos.x;
				tempPos.z = startingPos.z;
				tempPos.y = transform.position.y + speed1 * Time.deltaTime;

				if (tempPos.y > maxHeight) {

					tempPos.y = maxHeight;
					speed1 = -speed1;

				}

				if (tempPos.y < startingPos.y) {

					tempPos.y = startingPos.y;
					speed1 = -speed1;

				}

				transform.position = tempPos;
				break;
			
			case ObstaclePos.pos2:
				tempPos.x = startingPos.x;
				tempPos.z = startingPos.z;
				tempPos.y = transform.position.y + speed2 * Time.deltaTime;
			
				if (tempPos.y > maxHeight) {
				
					tempPos.y = maxHeight;
					speed2 = -speed2;
				
				}
			
				if (tempPos.y < startingPos.y) {
				
					tempPos.y = startingPos.y;
					speed2 = -speed2;
				
				}
			
				transform.position = tempPos;
				break;
			
			case ObstaclePos.pos3:
				tempPos.x = startingPos.x;
				tempPos.z = startingPos.z;
				tempPos.y = transform.position.y + speed3 * Time.deltaTime;
			
				if (tempPos.y > maxHeight) {
				
					tempPos.y = maxHeight;
					speed3 = -speed3;
				
				}
			
				if (tempPos.y < startingPos.y) {
				
					tempPos.y = startingPos.y;
					speed3 = -speed3;
				
				}
			
				transform.position = tempPos;
				break;
			
			case ObstaclePos.pos4:
				tempPos.x = startingPos.x;
				tempPos.z = startingPos.z;
				tempPos.y = transform.position.y + speed4 * Time.deltaTime;
			
				if (tempPos.y > maxHeight) {
				
					tempPos.y = maxHeight;
					speed4 = -speed4;
				
				}
			
				if (tempPos.y < startingPos.y) {
				
					tempPos.y = startingPos.y;
					speed4 = -speed4;
				
				}
			
				transform.position = tempPos;
				break;
			
			}

		}
	}

}
