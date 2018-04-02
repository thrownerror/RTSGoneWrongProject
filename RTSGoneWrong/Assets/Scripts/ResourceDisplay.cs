using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceDisplay : MonoBehaviour {

    // Use this for initialization
    
    public Text resourcetext;
    private bool gameover = false;
    private int favour = 100;
    
    
	void Start () {
        StartCoroutine(slowtime());

    }
	
	// Update is called once per frame
	void Update () {
        Display();
	}

    void Display()
    {
        
        Debug.Log("\nResource: " + favour);
        resourcetext.text = "Resource: " + favour;

    }
    void Increase_favour()
    {

        favour = favour + 1;
      
       
    }
    IEnumerator slowtime()
    {
        WaitForSeconds wait = new WaitForSeconds(5);
        while (!gameover)
        {

            Increase_favour();
            yield return wait;
        }
    }

}
