using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceDisplay : MonoBehaviour {

    // Use this for initialization
    
    public Text resourcetext;
    private bool gameover = false;
    public static int favour = 100;
	private float favourPerSecond = 4.0f;
	private float timer;
    
    
	void Start () {
        //StartCoroutine(slowtime());

		timer = 0.0f;

    }
	
	// Update is called once per frame
	void Update () {
        Display();

		if (timer > (1.0f / favourPerSecond))
		{
			favour += 1;
			timer = 0.0f;
		}

		timer += Time.deltaTime;
	}

    void Display()
    {
        
        Debug.Log("\nResource: " + favour);
		gameObject.GetComponent<Text>().text = "Resource: " + favour;

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
