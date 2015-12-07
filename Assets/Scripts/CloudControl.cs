using UnityEngine;
using System.Collections;

public class CloudControl : MonoBehaviour {

	public GameObject[] cloudPrefabs;
	public float cloudSpeed;
	public Vector3 cloudPosMax;

	private int numClouds = 20;
	private Vector3 cloudPosMin;
	private float cloudScaleMax;
	private float cloudScaleMin;
	private GameObject[] cloudInstances;

	void Awake(){

		cloudPosMin = this.transform.position; cloudPosMin.x -= 20;
		cloudScaleMin = 1;
		cloudScaleMax = 5;

		cloudInstances = new GameObject[numClouds];

		GameObject cloud;

		for (int i = 0; i < numClouds; i++) {

			int prefabNum = Random.Range(0, cloudPrefabs.Length);
			cloud = Instantiate (cloudPrefabs[prefabNum]) as GameObject;

			Vector3 cPosTemp = Vector3.zero;
			cPosTemp.x = Random.Range( cloudPosMin.x, cloudPosMax.x );
			cPosTemp.y = Random.Range( cloudPosMin.y, cloudPosMax.y );

			float scaleU = Random.value; //finds a scale for the clouds
			float scaleVal = Mathf.Lerp( cloudScaleMin, cloudScaleMax, scaleU); //sets the scale so it has to be between the bounds of the the cloud area
			cPosTemp.y = Mathf.Lerp( cloudPosMin.y, cPosTemp.y, scaleU); //sets smaller clouds closer to the ground
			cPosTemp.z = 100 - 90 * scaleU; //smaller clouds are farther away

			cloud.transform.position = cPosTemp;
			cloud.transform.localScale = Vector3.one * scaleVal;

			cloudInstances[i] = cloud; //puts the child in an array of clouds

		}

	}

	void Update(){
		
		foreach(GameObject cloud in cloudInstances){
			
			float scaleVal = cloud.transform.localScale.x;
			Vector3 cPos =cloud.transform.position;

			cPos.x -= scaleVal * Time.deltaTime * cloudSpeed;

			if(cPos.x <= cloudPosMin.x){ cPos.x = cloudPosMax.x; }
		
			cloud.transform.position = cPos;

		}
		
	}

}
