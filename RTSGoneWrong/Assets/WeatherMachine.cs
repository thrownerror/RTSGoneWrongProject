using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherMachine : MonoBehaviour {

    //
    private GameObject[] playerUnits;
    bool downPressed;
    bool upPressed;
    bool leftPressed;
    bool rightPressed;
    float windIntensity = 0.0f;
    public float windForce = 10.0f;
    public bool affectsBothTeams = false;
	// Use this for initialization
	void Start () {
        downPressed = false;
        upPressed = false;
        leftPressed = false;
        rightPressed = false;

	}
	
	// Update is called once per frame
	void Update () {
        windIntensity = Random.value * windForce;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            leftPressed = true;
            applyDirection("left");
            Debug.Log("Left");
        }
        else
        {
            leftPressed = false;
        }
       
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            upPressed = true;
            applyDirection("up");
            Debug.Log("Up");
        }
        else
        {
            upPressed = false;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rightPressed = true;
            applyDirection("right");

            Debug.Log("Right");
        }
        else
        {
            rightPressed = false;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            downPressed = true;
            applyDirection("down");
            Debug.Log("Down");
        }
        else
        {
            downPressed = false;
        }
    }
    void applyDirection(string dir)
    {
        playerUnits = GameObject.FindGameObjectsWithTag("unit");
        Vector3 direction = new Vector3(0, 0, 0);
        switch (dir)
        {
            case "up":
                direction = new Vector3(0, windIntensity, 0);
                break;
            case "left":
                direction = new Vector3(-1 * windIntensity, 0, 0);
                break;
            case "right":
                direction = new Vector3(windIntensity, 0, 0);
                break;
            case "down":
                direction = new Vector3(0, -1 * windIntensity, 0);
                break;
        }
        foreach (GameObject u in playerUnits){
            if (affectsBothTeams)
            {
                u.GetComponent<UnitGeneralBehavior>().applyWind(direction);

            }
            else
            {
                if (u.GetComponent<UnitGeneralBehavior>().GetDirection())
                {
                    u.GetComponent<UnitGeneralBehavior>().applyWind(direction);
                }
            }
        }
    }
}
