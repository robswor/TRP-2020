using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public enum Direction { Up, Down, Left, Right };//direction we are facing

    public bool onlyRight;//if it is in right mode or not
    public bool hasKey;
    bool canMove = true;

    public AudioSource cantMove;
	public AudioSource jumping;
	public AudioSource playerwon;
	public AudioSource buttonPress;
	public AudioSource keyPickUp;
	public AudioSource fanNoise;

    private Tile tileBelow;
    public BlockManager blockManager;

    //Only Go Right delegate
	public delegate void VoidBoolParam(bool mode);
	public static VoidBoolParam RightModeUpdate;


    //camera stuff
    public Camera moveCam, upCam, downCam, rightCam, leftCam;
    Camera newCam;
    bool cameraMoving = false;
    float camTimer = 0f;
    public GameObject levelCenter;

    public Direction directionFacing = Direction.Up;

	public GameObject levelSelectionPausedPanel;
	public GameObject levelSelectionWonPanel;
	public GameObject mainCamera;
	bool won = false;
	bool playerWonMusicPlayed = false;
	bool pickedUpKeyPlayed = false;

    bool moving1 = false, moving2 = false;
    float moveTimer = 0f;
    float moveSpeed = .5f;

	bool paused = false;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(whereDoIGo());
        moveCam.transform.LookAt(levelCenter.transform);
		
		RightModeUpdate(onlyRight);
		FindTileBelow();
    }

	//Read player controls

    // Update is called once per frame
    void Update()
    {
        if (cameraMoving) canMove = false;
		/* TODO: Move into a pause script
		if(Input.GetKeyDown(KeyCode.Escape) && !won) {
			if(paused == true){
				paused = false;
				levelSelectionPausedPanel.gameObject.SetActive (false);
			}
			else if(paused == false){
				paused = true;
				levelSelectionPausedPanel.gameObject.SetActive (true);
			}
		}*/
			
        //account for block undulation when standing still
		// TODO: Its own script, maybe?
        if (!moving1 && !moving2)
        {
            float wavey = this.transform.position.y - tileBelow.transform.position.y - .65f;
            if (wavey != 0f)
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - wavey, this.transform.position.z);
        }
        

        /////////////
        //MOVEMENT///
        /////////////
        //camera moving
        if (camTimer < 1.8f && cameraMoving)
        {
            camTimer += Time.deltaTime;
            //            moveCam.transform.position = Vector3.Slerp(moveCam.transform.position, newCam.transform.position, 1.5f * Time.deltaTime);
            moveCam.transform.position = Vector3.Slerp(moveCam.transform.position, newCam.transform.position, camTimer / 12f);
            Debug.Log(camTimer.ToString());
            moveCam.transform.LookAt(levelCenter.transform);
        }
        else
        {
            camTimer = 0f;
            cameraMoving = false;
            canMove = true;
        }

        //no longer movement
    }

    IEnumerator movementCoroutine(Tile newTile)
    {		
		newTile.TileAction();
		yield return new WaitForSeconds(0.5f);
		
        tileBelow = newTile;
        moving1 = true;
		jumping.Play ();
    }

    IEnumerator whereDoIGo()
    {
        //to add some indication of a tile being able to be moved to;
        yield return null;
    }
    

    void endScene()
    {
		if (!playerWonMusicPlayed) {
			playerwon.Play ();
			playerWonMusicPlayed = true;
		}
		levelSelectionWonPanel.gameObject.SetActive (true);
		mainCamera.GetComponent<cameraRotation> ().enabled = true;
    }

	public void FindTileBelow()
	{
		// Forces an error if something's wrong
		tileBelow = null;

		RaycastHit hit;
		Vector3 castPos = transform.position;
		castPos.y += 0.5f;
		if (Physics.Raycast(castPos, Vector3.down, out hit, 1.0f))
		{
			tileBelow = hit.transform.GetComponent<Tile>();
		}
		if (tileBelow == null)
		{
			Debug.LogError("No tile below the player.");
		}
	}

	public Tile GetTileBelow()
	{
		return tileBelow;
	}

	public void TriggerTileBelow()
	{
		tileBelow.TileAction();
	}

}


