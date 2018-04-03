using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour {

	public enum unitTypes
    {
        tank,
        healer,
        barbarian,
        sniper,
        flagbearer
    }

    [SerializeField] public unitTypes thisUnitType;
	[SerializeField] public int unitHealth;
	[SerializeField] public int unitAttack;
	[SerializeField] public float unitSpeed;
	[SerializeField] public int unitCost;
	[SerializeField] public float unitRange;


    // Use this for initialization
    void Start() 
	{
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
			case unitTypes.tank:
				break;
			case unitTypes.healer:
				break;
			case unitTypes.barbarian:
				break;
			case unitTypes.sniper:
				break;
			case unitTypes.flagbearer:
				break;
        }
    }
}
