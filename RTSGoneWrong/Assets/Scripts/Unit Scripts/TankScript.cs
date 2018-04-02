using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankScript : MonoBehaviour {


    public int Health = 150;
    public int Attack = 2;
    public int Speed = 3;
    public int Cost = 2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.position += new Vector3(Speed * 0.01f, 0,0);

	}
}
