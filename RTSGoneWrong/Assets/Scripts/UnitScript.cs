using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour {

    enum unitTypes
    {
        tank,
        healer,
        barbarian,
        sniper,
        flagbearer
    }

    private unitTypes thisUnitType;
    private int unitHealth;
    private int unitAttack;
    private int unitSpeed;
    private int unitCost;
    private Texture2D unitSprite;


    // Use this for initialization
    void Start () {
        switch (thisUnitType)
        {

        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void NormalBehavior()
    {
        switch (thisUnitType)
        {

        }
    }
}
