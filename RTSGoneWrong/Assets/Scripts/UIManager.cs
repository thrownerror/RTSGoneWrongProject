using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {


	//1 through 8 is for units, 9 is for the weather machine
	public static int currentSelectedUnit;
	//add the highlight perimeter into this gameobject spot in order to track its location
	public GameObject highlightPerimeter;
	//all the buttons on the screen
	public GameObject[] buttons;
	//the scene's main camera, please drag into script
	[SerializeField] private Camera mainCamera;
	[SerializeField] private float cameraSpeed;

	// Use this for initialization
	void Start() 
	{
		currentSelectedUnit = 1;
	}
	
	// Update is called once per frame
	void Update() 
	{
		highlightPerimeter.GetComponent<RectTransform>().position = buttons[currentSelectedUnit - 1].GetComponent<RectTransform>().position;
		MoveCamera();
	}

	public void PressButton(int buttonNum)
	{
		currentSelectedUnit = buttonNum;
	}

	private void MoveCamera()
	{
		if (Input.GetAxis("Vertical") > 0) 
		{
			mainCamera.transform.position += new Vector3(0.0f, cameraSpeed * Time.deltaTime, 0.0f);
		}
		else if (Input.GetAxis("Vertical") < 0) 
		{
			mainCamera.transform.position += new Vector3(0.0f, -cameraSpeed * Time.deltaTime, 0.0f);
		}

		if (Input.GetAxis("Horizontal") > 0) 
		{
			mainCamera.transform.position += new Vector3(cameraSpeed * Time.deltaTime, 0.0f, 0.0f);
		}
		else if (Input.GetAxis("Horizontal") < 0) 
		{
			mainCamera.transform.position += new Vector3(-cameraSpeed * Time.deltaTime, 0.0f, 0.0f);
		}
	}
}
