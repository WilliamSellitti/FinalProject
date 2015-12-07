using UnityEngine;
using System.Collections;

public class SlingShot : MonoBehaviour {

	static public SlingShot S;

	public ShotScript prefabShot; //this is the name of the shot prefab
	public float speed;
	public bool objectsBelowThisBooleanAreSetByTheGameButNeedToBePublic;

	public GameObject launchPoint; //halo object

	private ShotScript shot; //this is the shot object
	private bool aimingMode;
	private Vector3 launchPos; //This is the starting point of the shot

	void Awake(){

		S = this;

		launchPoint = transform.Find ("LaunchPoint").gameObject; //sets the halo
		launchPoint.SetActive (false); //turns the halo off

		launchPos = launchPoint.transform.position;

	}

	void OnMouseEnter(){

		if( MissionDemolition.S.mode == GameMode.playing )
			launchPoint.SetActive (true); //turns the halo on

	}

	void OnMouseExit(){

		if( MissionDemolition.S.mode == GameMode.playing )
			launchPoint.SetActive (false); //turns the halo off

	}

	void OnMouseDown(){

		if (MissionDemolition.S.mode == GameMode.playing) {

			aimingMode = true;
		shot = Instantiate (prefabShot) as ShotScript; //instantiates the shot/
		shot.type = ShotType.normal;

		if (MissionDemolition.S.level >= MissionDemolition.S.castles.Length + 1) {

			int x = Random.Range (0, 5);
			if (x == 0) {

				shot.type = ShotType.laser;
				 
			}

		} //shot.type = ShotType.laser;

		shot.transform.position = launchPos; //puts the shot in the area
		shot.rigidbody.isKinematic = true;

		}

	}

	void Update(){

		if (!aimingMode) { //if mouse down click over appropriate area did not happen
			return;
		}

		Vector3 mousePos2D = Input.mousePosition;							/* 																  */
		mousePos2D.z = - Camera.main.transform.position.z;					/* sets the mouses position relative to the X and Y of the screen */
		Vector3 mousePos3D = Camera.main.ScreenToWorldPoint ( mousePos2D ); /* 																  */

		Vector3 mouseDelta = mousePos3D - launchPos;
		float pullBack = this.GetComponent<SphereCollider> ().radius;

		if (mouseDelta.magnitude > pullBack) {

			mouseDelta.Normalize();
			mouseDelta *= pullBack;

		}

		Vector3 shotPos = launchPos + mouseDelta;
		shot.transform.position = shotPos;

		if (Input.GetMouseButtonUp (0)) {

			MissionDemolition.ShotFired();
			aimingMode = false;
			shot.rigidbody.isKinematic = false;
			shot.rigidbody.velocity = -mouseDelta * speed;

			FollowCam.S.poi = shot.gameObject;

			shot = null;

		}

	}

}
