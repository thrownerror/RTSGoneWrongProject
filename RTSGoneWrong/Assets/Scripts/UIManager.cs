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

    private GameObject unit1;
    private GameObject unit2;

    // Selecting Base
    public int selectedBaseIndex = 1;
    private GameObject selectedBase;


    // Number of Bases
    public int numBases = 3;



    // Use this for initialization
    void Start()
    {
        currentSelectedUnit = 1;
    }

    // Update is called once per frame
    void Update()
    {

        MoveCamera();



        if (Input.GetKeyDown(KeyCode.Z))
        {
            selectedBaseIndex++;
            GameObject selector = GameObject.Find("BaseSelector");
            if (selectedBaseIndex > numBases)
            {
                selectedBaseIndex = 1;
            }
            selector.transform.position = new Vector3(selector.transform.position.x, GameObject.Find("Bases").transform.GetChild(selectedBaseIndex).position.y, selector.transform.position.z);
        }



    }

    public void PressButton(int buttonNum)
	{
		print(buttonNum);
		currentSelectedUnit = buttonNum;

        //if (currentSelectedUnit == 2)
        {
            unit2 = GameObject.Find("Tank");
            selectedBase = GameObject.Find("Bases").transform.GetChild(selectedBaseIndex).gameObject;
            Instantiate(unit2, selectedBase.transform.position, Quaternion.identity);
        }

		highlightPerimeter.GetComponent<RectTransform>().position = buttons[currentSelectedUnit - 1].GetComponent<RectTransform>().position;
		highlightPerimeter.GetComponent<RectTransform>().position += new Vector3(0.0f, 0.0f, -1.0f);
        
		GameObject myEventSystem = GameObject.Find("EventSystem");
		myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
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
