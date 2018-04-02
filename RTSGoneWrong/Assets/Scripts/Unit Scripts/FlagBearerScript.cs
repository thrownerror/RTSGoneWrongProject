using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagBearerScript : MonoBehaviour {

    public int Health = 100;
    public int Attack = 9;
    public int Speed = 5;
    public int Cost = 3;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        transform.position += new Vector3(Speed * 0.01f, 0, 0);

    }
}
