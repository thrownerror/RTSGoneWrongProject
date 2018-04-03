using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This class is deprecated and not implemented
/// </summary>
public class ResourceGen : MonoBehaviour {

    // Use this for initialization
    public int favour = 100;
    private bool gameover = false;
	void Start () {
        StartCoroutine(slowtime());

    }
	
	// Update is called once per frame
	void Update () {

        
    }

    void Increase_favour()
    {
      
        favour = favour + 1;
        GetFavour();
       // Debug.Log("\nResource: " + favour);
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

    public int GetFavour()
    {
        return favour;
    }
}
