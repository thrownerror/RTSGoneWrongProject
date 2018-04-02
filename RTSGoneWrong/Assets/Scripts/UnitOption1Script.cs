using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitOption1Script : MonoBehaviour {

    public Button button;
    private GameObject unit1;
    public GameObject selectedBase;
    

	// Use this for initialization
	void Awake () {
        button = GetComponent<Button>();
        button.onClick.AddListener(Creation);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void Creation()
    {
        unit1 = GameObject.Find("Tank");

        Instantiate(unit1, selectedBase.transform.position, Quaternion.identity);
    }

    
}
