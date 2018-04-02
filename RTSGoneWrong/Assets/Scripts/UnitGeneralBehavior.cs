using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGeneralBehavior : MonoBehaviour {

    //determines if this unit has the goal of going to the right
    // Serialized for testing. Spawn should determine this
    // if goesRight = true, it is player. else enemy AI
    [SerializeField] private bool goesRight;
    //a value ranging from 0 to 100
    [SerializeField] private float obedience;

    //the radius of how far away this unit can see
    [SerializeField] private float visionRadius;
    //if this is true, vision is a circle, not a semicircle
    [SerializeField] private bool canSeeBehind;

    //how fast this unit will move
    [SerializeField] private float speed;
    //how fast this unit will move in a panic
    [SerializeField] private float speedPanic;
    private Vector3 goal;

    private GameObject enemyBase;

    private List<GameObject> otherUnits;



    //the following are random actions the unit can do
    [SerializeField] private float wanderChance;
    [SerializeField] private float seekRandomSpotChance;
    [SerializeField] private float seekEnemyBaseChance;
    [SerializeField] private float standStillChance;
    [SerializeField] private float goToClosestUnitChance;
    [SerializeField] private bool isDead;
    private Vector3 moveDirection;

    private bool isAttacking;
    private GameObject attackingEnemy;

    private bool isWandering;

    private float timer;
    private float splitTimer;
    [SerializeField] private float wanderTimesPerPeriod;
    [SerializeField] private float timeTillChangeDecision;

    // Use this for initialization
    void Start()
    {
        isDead = false;
        timer = 0.0f;
        attackingEnemy = null;
        goal = new Vector3(0.0f, 0.0f, 0.0f);

        isWandering = false;

        isAttacking = false;
        otherUnits = new List<GameObject>();

        GameObject[] allUnits = GameObject.FindGameObjectsWithTag("Unit");

        for (int i = 0; i < allUnits.GetLength(0); i++)
        {
            if (allUnits[i] != gameObject)
            {
                otherUnits.Add(allUnits[i]);
            }
        }

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
        // To check if it goes back to normal working
        if (isDead)
        {
            Destroy(gameObject);
        }
        if (isAttacking && attackingEnemy == null)
        {
            isAttacking = false;
        }
        if (!isAttacking)
        {
            MakeDecision();
            CircleDetectionFOrAttack();
            Seek(goal);

            timer += Time.deltaTime;
            splitTimer += Time.deltaTime;
        }
        else
        {
            //TODO: attack logic
            goal = attackingEnemy.transform.position;
            Seek(goal);
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

            if (rand <= obedience)
            {
                //do what player wants
            }
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
                    goal = new Vector3(Random.value * 10.0f - 5.0f, Random.value * 10.0f - 5.0f, 0.0f);
                    isWandering = false;
                }
                //seek the enemy base
                else if (anothaRand < wanderChance + seekRandomSpotChance + seekEnemyBaseChance)
                {
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

        //To avoid itersecting
        if (transform.position.x <= goal.x + 1.0f && transform.position.x >= goal.x - 1.0f && transform.position.y <= goal.y + 1.0f && transform.position.y >= goal.y - 1.0f)
        {
            return;
        }

        moveDirection = needsToMove;

        transform.position += Vector3.Normalize(moveDirection) * speed * Time.deltaTime;
    }

    //Checks in radius for enemies
    void CircleDetectionFOrAttack()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, visionRadius);

        if (hitColliders.GetLength(0) > 0)
        {
            List<Collider> enemyColliders = new List<Collider>();
            UnitGeneralBehavior unit = null;
            for (int i = 0; i < hitColliders.GetLength(0); i++)
            {
                unit = hitColliders[i].GetComponent(typeof(UnitGeneralBehavior)) as UnitGeneralBehavior;
                if (unit != null && unit.GetDirection() != goesRight)
                {
                    enemyColliders.Add(hitColliders[i]);
                }
            }
            if (enemyColliders.Count > 0)
            {
                float distance = 1000.0f;
                GameObject closestUnit = null;
                foreach (var collider in enemyColliders)
                {
                    if (Vector3.Distance(collider.transform.position, transform.position) < distance)
                    {
                        distance = Vector3.Distance(collider.transform.position, transform.position);
                        closestUnit = collider.gameObject;
                    }
                    isAttacking = true;
                }
                attackingEnemy = closestUnit;
                print("Attack!");
            }
        }
    }

    //To get to know if the unit is an enemy
    public bool GetDirection()
    {
        return goesRight;
    }

    public void applyWind(Vector3 direction)
    {
        goal = goal + direction;
    }

}
