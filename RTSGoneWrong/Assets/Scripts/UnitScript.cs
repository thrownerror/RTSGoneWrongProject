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
        flagbearer,
        homeBase
    }

    [SerializeField] public unitTypes thisUnitType;
	[SerializeField] public int unitHealth;
	[SerializeField] public int unitAttack;
	[SerializeField] public float unitSpeed;
	[SerializeField] public int unitCost;
	[SerializeField] public float unitRange;
    public bool isDamaged;

    public int unitMaxHealth;

    private Vector3 mouseRightClickPositionLatest;


    // Use this for initialization
    void Start() 
	{
        unitMaxHealth = unitHealth;
        mouseRightClickPositionLatest = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1))
            mouseRightClickPositionLatest = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));

        if (unitHealth <=0 )
        {
            Destroy(gameObject);
        }

        isDamaged = (unitHealth < unitMaxHealth);
    }

    public Vector3 NormalBehavior()
    {
        return mouseRightClickPositionLatest;
    }

}
