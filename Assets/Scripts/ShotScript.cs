using UnityEngine;
using System.Collections;

public enum ShotType{ // V 2.0

	normal,
	laser,
	explosion // Not implemented

}

public class ShotScript : MonoBehaviour { // V 2.0

	public Material specialMat;
	public Material normalMat;
	public GameObject Laser;

	public ShotType type = ShotType.normal;

	private bool firedLaser;

	void Start(){

		switch (type) {
			
		case ShotType.laser:
			firedLaser = false;
			this.renderer.material = specialMat;
			break;
			
		default:
			this.renderer.material = normalMat;
			break;
			
		}

	}

	void OnCollisionEnter(){

		switch (type) {

		case ShotType.laser:
			if(!firedLaser){
				GameObject laserGO = Instantiate(Laser) as GameObject;
				laserGO.transform.position = new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z);
				firedLaser = true;
			}
			break;

		default:
			break;

		}
		
	}
				                                        
}
