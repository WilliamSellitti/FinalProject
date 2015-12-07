using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {

	static public FollowCam S;
	public float maxX;
	public float minX;
	public bool objectsBelowThisBooleanAreSetByTheGameButNeedToBePublic;
	
	public GameObject poi;

	private Vector3 originalPos;
	private float camZ;
	private float cameraHeight; //also determines the minimum heigh of the camera and is used in sizing the camera
	
	void Awake(){
		
		S = this;
		camZ = -20.5f;
		originalPos = transform.position;
		cameraHeight = this.camera.orthographicSize;
		
	}
	
	void FixedUpdate(){
		if (MissionDemolition.S.mode == GameMode.playing) {

			if (poi == null) { 

				SetToOrigin ();
		
			} else { // V 2.0 made a lot of changes here

				Vector3 destination = Vector3.Lerp (transform.position, poi.transform.position, 0.1f);
				destination.z = camZ;

				if (poi.transform.position.x > transform.position.x) { //if the shot is ahead of the camera, move the camera

					if (destination.x > maxX) {

						destination.x = maxX; 
				
					} //if the camera is within bounds, move the camera

				}

				if (destination.y < originalPos.y) { //if the shot is underneath a certain point the camera does not move vertically
				
					destination.y = originalPos.y; 
				
				} //if the camera is within bounds, move the camera

				if (destination.x < minX && poi.tag == "Shot") {

					poi = null;

				}

				if (poi != null && poi.rigidbody != null) { 

					if (poi.rigidbody.IsSleeping ()) { 

						poi = null; 
						return; 
				
					}
			
				}

				transform.position = destination; //move the camera

			}



			if (transform.position.y > originalPos.y) {

				this.camera.orthographicSize = transform.position.y - originalPos.y + cameraHeight;

			} 

		}
	}

	void SetToOrigin(){

		transform.position = originalPos;
		this.camera.orthographicSize = cameraHeight;

	}
}
