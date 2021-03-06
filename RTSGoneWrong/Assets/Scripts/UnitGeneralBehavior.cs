﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGeneralBehavior : MonoBehaviour {

    //determines if this unit has the goal of going to the right
    // Serialized for testing. Spawn should determine this
    // if goesRight = true, it is player. else enemy AI
    [SerializeField] public bool goesRight;
    //a value ranging from 0 to 100
    [SerializeField] private float obedience;

    //the radius of how far away this unit can see
    [SerializeField] private float visionRadius;

    //how fast this unit will move
    [SerializeField] private float speed;
    //how fast this unit will move in a panic
    [SerializeField] private float speedPanic;
    private Vector3 goal;

	private UnitScript myUnitScript;

    private GameObject enemyBase;

	//the following are random actions the unit can do
	[SerializeField] private float wanderChance;
	[SerializeField] private float seekRandomSpotChance;
	[SerializeField] private float seekEnemyBaseChance;
	[SerializeField] private float standStillChance;
	[SerializeField] private float goToClosestUnitChance;
    [SerializeField] private bool isDead;
    [SerializeField] private float attackSpeed;
    private Vector3 moveDirection;

    private bool isAttacking;
    private GameObject attackingEnemy;
    private float inflictDamageTimer;
    private bool isWandering;

    private float timer;
    private float splitTimer;
    [SerializeField] private float wanderTimesPerPeriod;
    [SerializeField] private float timeTillChangeDecision;

    // Use this for initialization
    void Start()
    {
        isDead = false;
        timer = timeTillChangeDecision - 0.2f;
        attackingEnemy = null;
        goal = Vector3.zero;
        inflictDamageTimer = 0.0f;
        myUnitScript = gameObject.GetComponent<UnitScript>();

        isWandering = false;

		isAttacking = false;

        if (goesRight)
        {
            moveDirection = Vector3.Normalize(new Vector3(speed, 0.0f, 0.0f));
            enemyBase = GameObject.FindGameObjectWithTag("EnemyBase");
        }
        else
        {
            moveDirection = Vector3.Normalize(new Vector3(-speed, 0.0f, 0.0f));
            enemyBase = GameObject.FindGameObjectWithTag("PlayerBase");
        }

    }

    // Update is called once per frame
    void Update()
    {
        // To check if it goes back to normal working. For testing
        if (isDead)
        {
            Destroy(gameObject);
        }

        if (!isAttacking)
		{
			MakeDecision();

            CircleDetectionForAttack();
            Seek(goal);

            timer += Time.deltaTime;
            splitTimer += Time.deltaTime;
        }
        else
        {
            if (attackingEnemy == null)
            {
                isAttacking = false;
                inflictDamageTimer = 0.0f;
            }
            else
            {
                goal = attackingEnemy.transform.position;
                Seek(goal);
                inflictDamageTimer += Time.deltaTime;
            }
        }

        if (isWandering)
        {
            Wander();
        }


    }

    private void MakeDecision()
    {
        if (timer > timeTillChangeDecision)
        {
            float rand = Random.value * 100.0f;

			//obey, but only allied units
			if (goesRight == true && rand <= obedience)
			{
                print("Obey");
				goal = gameObject.GetComponent<UnitScript>().NormalBehavior();
                print(goal);
			} 
			//disobey
			else
			{
				//does whatever it wants
				float anothaRand = Random.value * (wanderChance + seekRandomSpotChance + seekEnemyBaseChance + standStillChance + goToClosestUnitChance);

				//wander
				if (anothaRand < wanderChance)
				{
					print("Wandering!");
					isWandering = true;
				} 
				//seek a random spot on the map
				else if (anothaRand < wanderChance + seekRandomSpotChance)
				{
					print("Random Spot!");
					goal = new Vector3(Random.value * 120.0f - 60.0f, Random.value * 34.0f - 17.0f, 0.0f);
					isWandering = false;
				} 
				//seek the enemy base
				else if (anothaRand < wanderChance + seekRandomSpotChance + seekEnemyBaseChance)
				{
                    if (goesRight)
                    {
                        enemyBase = GameObject.FindGameObjectWithTag("EnemyBase");
                    }
                    else
                    {
                        enemyBase = GameObject.FindGameObjectWithTag("PlayerBase");
                    }
                    print("Enemy Base!");
					//TODO: find the enemy base
					goal = enemyBase.transform.position;
					isWandering = false;
				} 
				//stand still
				else if (anothaRand < wanderChance + seekRandomSpotChance + seekEnemyBaseChance + standStillChance)
				{
					print("Stand Still!");
					goal = transform.position;
					isWandering = false;
				} 
				//go to the closest unit
				else
				{
					print("Closest Unit!!");
					GameObject other = null;
					float distance = 1000.0f;

                    List<GameObject> otherUnits = new List<GameObject>();

                    GameObject[] allUnits = GameObject.FindGameObjectsWithTag("Unit");

                    for (int i = 0; i < allUnits.GetLength(0); i++)
                    {
                    	if (allUnits[i] != gameObject)
                    	{
                    		otherUnits.Add(allUnits[i]);
                    	}
                    }
                    foreach (GameObject unit in otherUnits)
					{
						if (Vector3.Distance(unit.transform.position, transform.position) < distance)
						{
							distance = Vector3.Distance(unit.transform.position, transform.position);
							other = unit;
						}
					}

                    if (other != null)
                    {
                        goal = other.transform.position;
                    }

                    isWandering = false;
                }
            }

            timer = 0.0f;
        }
    }

    private void Wander()
    {
        if (splitTimer > timeTillChangeDecision / wanderTimesPerPeriod)
        {
            goal = new Vector3(Random.value * 10.0f - 5.0f, Random.value * 10.0f - 5.0f, 0.0f);

            splitTimer = 0.0f;
        }
    }

    private void Seek(Vector3 myGoal)
    {
        goal = myGoal;

        Vector3 needsToMove = goal - transform.position;

        //To avoid intersecting
		float rangeToMove = 0.5f;
		if (isAttacking)
		{
			rangeToMove = myUnitScript.unitRange;
		}
		if ((transform.position.x <= goal.x + rangeToMove && transform.position.x >=  goal.x - rangeToMove) && 
			(transform.position.y <= goal.y + rangeToMove && transform.position.y >= goal.y - rangeToMove ))
		{
            if (isAttacking)
            {
                InflictDamage();
            }
			return;
		}

        moveDirection = needsToMove;

        transform.position += Vector3.Normalize(moveDirection) * speed * Time.deltaTime;
    }

    //Checks in radius for enemies
    void CircleDetectionForAttack()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, visionRadius);

        if (hitColliders.GetLength(0) > 0)
        {
            List<Collider2D> enemyColliders = new List<Collider2D>();
            UnitGeneralBehavior unit = null;
            string toSeekBase = (goesRight ? "EnemyBase" : "PlayerBase");
            for (int i = 0; i < hitColliders.GetLength(0); i++)
            {
				unit = hitColliders[i].GetComponent<UnitGeneralBehavior>();
                if (myUnitScript.thisUnitType != UnitScript.unitTypes.healer)
                {
                    if ((unit != null && unit.GetDirection() != goesRight) || 
                        (hitColliders[i].gameObject.CompareTag(toSeekBase)))
                    {
                        enemyColliders.Add(hitColliders[i]);
                    }
                }
                else
                {
                    if (unit != null && unit.GetDirection() == goesRight)
                    {
                        enemyColliders.Add(hitColliders[i]);
                    }
                }
            }
            if (enemyColliders.Count > 0)
            {
                float distance = 1000.0f;
                GameObject closestUnit = null;
                foreach (var collider in enemyColliders)
                {
                    if (myUnitScript.thisUnitType == UnitScript.unitTypes.healer && !(collider.gameObject.GetComponent<UnitScript>().isDamaged))
                    {
                        continue;
                    }
                    if ((Vector3.Distance(collider.transform.position, transform.position) < distance))
                    {
                        distance = Vector3.Distance(collider.transform.position, transform.position);
                        closestUnit = collider.gameObject;
                    }
                }
                if (closestUnit != null)
                {
                    isAttacking = true;
                    attackingEnemy = closestUnit;
                    print("Attack!");
                }
            }
        }
    }

    //To get to know if the unit is an enemy
    public bool GetDirection()
    {
        return goesRight;
    }

    void InflictDamage()
    {
        if (inflictDamageTimer >= attackSpeed)
        {
            if (myUnitScript.thisUnitType != UnitScript.unitTypes.healer)
            {
                attackingEnemy.GetComponent<UnitScript>().unitHealth -= myUnitScript.unitAttack;
            }
            else
            {
                attackingEnemy.GetComponent<UnitScript>().unitHealth += myUnitScript.unitAttack;
                if (attackingEnemy.GetComponent<UnitScript>().unitHealth >= attackingEnemy.GetComponent<UnitScript>().unitMaxHealth)
                {
                    attackingEnemy.GetComponent<UnitScript>().unitHealth = attackingEnemy.GetComponent<UnitScript>().unitMaxHealth;
                }
            }
            inflictDamageTimer = 0.0f;
        }
    }

    public void applyWind(Vector3 direction)
    {
        goal = goal + direction;
    }

}