using UnityEngine;
using System.Collections;

public enum GameMode {

	start,
	levelSelect,
	pause,
	idle, 
	playing, 
	levelEnd

}

public class MissionDemolition : MonoBehaviour {

	static public MissionDemolition S;

	public GameObject[] castles;
	public Obstacle obstacle; //V 2.0
	public GUIText gtLevel;
	public GUIText gtScore;
	public GUIText gtEndResult;
	public GUIText gtTotalShotsSoFar;

	public string showing = "Both";
	public int level = 0;
	public GameMode mode = GameMode.idle;

	private int maxLevel;
	private int shotsTaken;
	private int totalShotsTaken; //V 2.0
	private GameObject castle;
	private Obstacle[] obstacles = new Obstacle [4]; //v 2.0
	private bool restart = false; //V 2.0
	private int lastSavedShotsTaken;

	static private GameObject view;

	void Start(){

		S = this;
		gtEndResult.enabled = false;
		maxLevel = castles.Length * obstacles.Length;

		StartGame ();

	}

	void StartGame(){

		mode = GameMode.start;
		totalShotsTaken = 0;
		level = 0;

	}

	Rect[] LevelSelect(){

		Rect[] buttonLevel = new Rect[castles.Length * obstacles.Length];
		
		int x = Screen.width/2 - 127;
		int y = Screen.height/2 - 49;
		
		int count = 0;
		for(int i = 0; i < buttonLevel.Length; i++){
			
			buttonLevel[i] = new Rect (x, y, 50, 24);
			y += 25;
			
			if (count == 4){
				
				y = Screen.height/2 - 49;
				x += 51;
				count = 0;
				
			}
			
			else 
				count++;
			
		}

		return buttonLevel;

	}

	void StartLevel(){

		lastSavedShotsTaken = totalShotsTaken;

		if ( castle != null ) Destroy ( castle );

		GameObject[] gos = GameObject.FindGameObjectsWithTag ("Shot");
		foreach (GameObject pTemp in gos) Destroy (pTemp);

		castle = Instantiate (castles [level % castles.Length] ) as GameObject;

		//V 2.0 obstacle code

		for (int i = 0; i < obstacles.Length; i++) { //deletes any current game objects

			if(obstacles[i] == null)
				break;

			Destroy(obstacles[i].gameObject);
			obstacles [i] = null;

		}

		for (int i = 0; i < (level)/castles.Length + 1; i++) { //adds whatever obstacles need to be added for this level
				
			if (obstacles [i] == null) {
				
				obstacles [i] = Instantiate (obstacle) as Obstacle;
				
				switch (i) {
					
				case 0:
					obstacles [i].posNum = ObstaclePos.pos1;
					break;
				case 1:
					obstacles [i].posNum = ObstaclePos.pos2;
					break;
				case 2:
					obstacles [i].posNum = ObstaclePos.pos3;
					break;
				case 3:
					obstacles [i].posNum = ObstaclePos.pos4;
					break;
					
				}
				
			}
			
		}

		//end of V 2.0 code

		shotsTaken = 0;

		SwitchView ("Both");
		ShotLine.S.Clear ();

		GoalBehavior.goalMet = false;

		ShowGT();

		mode = GameMode.playing;

	}

	void RestartLevel(){

		totalShotsTaken = lastSavedShotsTaken;
		
		if ( castle != null ) Destroy ( castle );
		
		GameObject[] gos = GameObject.FindGameObjectsWithTag ("Shot");
		foreach (GameObject pTemp in gos) Destroy (pTemp);
		
		castle = Instantiate (castles [level % castles.Length] ) as GameObject;
		
		shotsTaken = 0;
		
		SwitchView ("Both");
		ShotLine.S.Clear ();
		
		GoalBehavior.goalMet = false;
		
		ShowGT();
		
		mode = GameMode.playing;

	}

	void ShowGT(){

		if (mode == GameMode.playing) {

			gtLevel.text = "Level: " + (level + 1) + " of " + maxLevel;
			gtScore.text = "Shots taken: " + shotsTaken;
			gtTotalShotsSoFar.text = "Shots taken so far: " + totalShotsTaken;

		}

	}

	void Update(){

		//V 2.0 added restart button here
		if (restart) {

			gtEndResult.enabled = true;
			gtEndResult.text = "Total Number of shots taken: " + totalShotsTaken + "\nPress R to restart.";
			
			if (Input.GetKeyDown (KeyCode.R)) {
				
				Application.LoadLevel ("Scene_");
				
			}

		} 

		else {

			ShowGT ();
			
			if ( mode == GameMode.playing && GoalBehavior.goalMet ){
				
				mode = GameMode.levelEnd;
				SwitchView("Both");
				Invoke ("NextLevel", 2f);
				
			}

			if (Input.GetKeyUp(KeyCode.M)){

				mode = GameMode.pause;
				SwitchView("Both");

			}

		}

	}

	void NextLevel(){

		level++;

		if (level == maxLevel) {

			restart = true;

		} 

		else {

			StartLevel ();

		}

	}

	void OnGUI(){

		if (mode == GameMode.start) {

			Rect buttonRect = new Rect ((Screen.width / 2) - 100, (Screen.height / 2) - 51, 200, 50);
			Rect buttonRect2 = new Rect ((Screen.width / 2) - 100, (Screen.height / 2) + 1, 200, 50);
			
			if (GUI.Button (buttonRect, "Start Game")) {
				
				StartLevel ();
				
			}
			
			if (GUI.Button (buttonRect2, "Select level")) {
				
				mode = GameMode.levelSelect;
				
			}

			return;

		} 

		else if (mode == GameMode.levelSelect) {

			Rect[] buttonLevel = LevelSelect ();

			for (int i = 0; i < buttonLevel.Length; i++) {
				
				if (GUI.Button (buttonLevel [i], (i + 1) + "")) {

					level = i;
					mode = GameMode.start;

				}
				
			}

			return;

		} 

		else if (mode == GameMode.pause) {

			Rect buttonRect = new Rect ((Screen.width / 2) - 100, (Screen.height / 2) - 76, 200, 50);
			Rect buttonRect2 = new Rect ((Screen.width / 2) - 100, (Screen.height / 2) - 25, 200, 50);
			Rect buttonRect3 = new Rect ((Screen.width / 2) - 100, (Screen.height / 2) + 26, 200, 50);
			
			if (GUI.Button (buttonRect, "Back to Game")) {
				
				mode = GameMode.playing;
				
			}

			if (GUI.Button (buttonRect2, "Restart Game")) {
				
				StartGame ();
				
			}
			
			if (GUI.Button (buttonRect3, "Quit Game")) {
				
				Application.Quit();
				
			}
			
			return;

		}

		else {

			Rect buttonRect = new Rect ((Screen.width / 2) - 102, 10, 100, 24);
			Rect buttonRect2 = new Rect ((Screen.width / 2) + 2, 10, 100, 24);
			
			switch (showing) {
			case "Slingshot":
				if( GUI.Button( buttonRect, "Show Castle" ) ) SwitchView("Castle");
				if( GUI.Button( buttonRect2, "Show Field" ) ) SwitchView("Both");
				break;
				
			case "Castle":
				if( GUI.Button( buttonRect, "Show Field" ) ) SwitchView("Both");
				if( GUI.Button( buttonRect2, "Show Slingshot" ) ) SwitchView("Slingshot");
				break;
				
			case "Both":
				if( GUI.Button( buttonRect, "Show Slingshot" ) ) SwitchView("Slingshot");
				if( GUI.Button( buttonRect2, "Show Castle" ) ) SwitchView("Castle");
				break;
				
			}

			Rect restartLevelButton = new Rect ((Screen.width / 2) -50, 40, 100, 24);
			if( GUI.Button( restartLevelButton, "Restart Level" ) ) RestartLevel();

		}

	}

	static public void SwitchView( string eView ){

		S.showing = eView;
		switch (S.showing) {
		case "Slingshot":
			FollowCam.S.poi = null;
			break;
			
		case "Castle":
			FollowCam.S.poi = S.castle;
			break;
			
		case "Both":
			view = GameObject.Find("ViewBoth");
			FollowCam.S.poi = view;
			break;
		}

	}

	public static void ShotFired(){

		S.shotsTaken++;
		S.totalShotsTaken++; //V 2.0

	}

}
